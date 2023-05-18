using Common.Helper;
using Common.Model;
using Common.Model.CSVModels;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helper;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     Controller für Wissenstests.
    /// </summary>
    public class KnowledgeTestController : BfkdoControllerBase
    {
        private readonly BfkdoDbContext _dbContext;

        private readonly ILogger _logger;

        /// <summary>
        ///     Konstruktor des Controllers.
        /// </summary>
        /// <param name="dbContext">Injected Datenbankmodell.</param>
        /// <param name="logger">Logger.</param>
        public KnowledgeTestController(BfkdoDbContext dbContext, ILogger<KnowledgeTestController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Abfrage der Details einen Wissenstest.
        /// </summary>
        /// <param name="id">Id der Wissenstestung.</param>
        /// <returns>Details einer Wissenstestung.</returns>
        /// <response code="200">Returniert die Details des Wissenstest.</response>
        /// <response code="400">Wenn der Wissenstest mit der gegebenen Id nicht gefunden werden konnte.</response>
        /// <response code="401">Wenn der Benutzer nicht die richtigen Rechte für den Call hat.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpGet]
        [Route("api/knowledgetest/GetKnowledgeTestDetails/{id}")]
        public ActionResult GetKnowledgeTestDetails(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Requested Knowledgetest with Id {id}, which is not an valid Id!");
                return BadRequest($"Wissenstest mit der Id {id} ist nicht gültig und konnte nicht gefunden werden!");
            }

            var knowledgetest = _dbContext.TableKnowledgeTests
                .Include(t => t.Registrations).ThenInclude(t => t.KnowledgeLevel)
                .Include(t => t.Registrations).ThenInclude(t => t.Evaluations)
                .FirstOrDefault(t => t.Id == id);
            if (knowledgetest == null)
            {
                _logger.LogError($"Requested Knowledgetest with Id {id} was not found in the Database!", DateTime.Now.ToLongTimeString());
                return BadRequest($"Wissenstest mit der Id {id} konnte nicht gefunden werden!");
            }

            var model = new ModelKnowledgeTestDetails()
            {
                KnowledgeTestId = knowledgetest.Id,
                KnowledgeTestYear = knowledgetest.Designation
            };
            var testpersonsIds = knowledgetest.Registrations.GroupBy(t => t.TestpersonId);
            foreach (var testpersonId in testpersonsIds)
            {
                var testperson = _dbContext.TableTestpersons.FirstOrDefault(t => t.Id == testpersonId.Key);
                if (testperson != null!)
                {
                    var results = GetResultsForTestPerson(knowledgetest, testperson);
                    model.TestPersonResults.Add(new()
                    {
                        Name = testperson.FirstName + " " + testperson.LastName,
                        Station = testperson.FireDepartmentBranch,
                        Results = results
                    });
                }
                else
                {
                    _logger.LogError($"Requested Testperson with Id {id} was not found in the Database!", DateTime.Now.ToLongTimeString());
                }
            }

            return Ok(model);
        }

        /// <summary>
        ///     Abfrage der Liste der Wissenstests.
        /// </summary>
        /// <returns>Liste der Wissenstestung.</returns>
        /// <response code="200">Returniert die Liste der Wissenstests.</response>
        /// <response code="401">Wenn der Benutzer nicht die richtigen Rechte für den Call hat.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpGet]
        [Route("api/knowledgetest/GetKnowledgeTests")]
        public ActionResult GetKnowledgeTests()
        {
            var knowledgetests = _dbContext.TableKnowledgeTests.ToList();
            return Ok(knowledgetests.Select(t => new ModelKnowledgeTest()
            {
                Designation = t.Designation,
                Id = t.Id
            }).ToList());
        }

        /// <summary>
        ///     Lesen der Teilnehmer aus der CSV.
        /// </summary>
        /// <param name="data">Daten aus CSV Bytes und Wissenstest Id.</param>
        /// <returns>Ob Teilnehmer ausgelesen werden konnten.</returns>
        /// <response code="200">Returniert, dass Einträge aus der CSV ausgelesen werden konnten.</response>
        /// <response code="400">Wenn aus dem gebotetenen CSV Byte Array keine Einträge erstellt werden konnten.</response>
        /// <response code="401">Wenn der Benutzer nicht die richtigen Rechte für den Call hat.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [Route("api/knowledgetest/ImportRegistrations")]
        [HttpPost]
        public ActionResult ImportRegistrations([FromBody] ModelImportData data)
        {
            var registrations = CsvHandlingHelper.GetDataFromCsvByteArray<CsvRegistrationModel>(data.CsvData);
            var levels = _dbContext.TableKnowledgeLevels.ToList();
            foreach (var registration in registrations)
            {
                var personId = CreateOrGetTestPerson(registration);

                var levelId = ParseKnowledgeLevel(levels, registration.RegistratedLevel);
                if (levelId == -1)
                {
                    continue;
                }
                var tableregistration = new TableRegistration()
                {
                    TestpersonId = personId,
                    KnowledgeTestId = data.KnowledgeTestId,
                    KnowledgeLevelId = levelId
                };
                _dbContext.TableRegistrations.Add(tableregistration);
                _dbContext.SaveChanges();

                var criterias = _dbContext.TableEvaluationCriterias.Where(t => t.KnowledgeLevelId == levelId).ToList();
                foreach (var criteria in criterias)
                {
                    var evaluation = new TableEvaluation()
                    {
                        Evaluation = Common.Enums.EnumEvaluation.Ungraded,
                        EvaluationCriteriaId = criteria.Key,
                        EvaluationState = Common.Enums.EnumEvaluationState.Open,
                        RegistrationId = tableregistration.Id,
                    };
                    _dbContext.TableEvaluations.Add(evaluation);
                }
                _dbContext.SaveChanges();

            }

            return Ok(true);
        }

        /// <summary>
        ///     Gibt die Id des Wissenstest aus.
        /// </summary>
        /// <param name="levels">Wissentest-Stufen.</param>
        /// <param name="registratedLevel">Gewollte Stufe.</param>
        /// <returns>Id der Stufe</returns>
        private int ParseKnowledgeLevel(List<TableKnowledgeLevel> levels, string registratedLevel)
        {
            return levels.FirstOrDefault(t => t.Description == registratedLevel)?.Id ?? -1;
        }

        /// <summary>
        ///     Erstellt oder liest die Testperson.
        /// </summary>
        /// <param name="registration">Ausgelesene CSV Daten.</param>
        /// <returns>Id der Testperson.</returns>
        private int CreateOrGetTestPerson(CsvRegistrationModel registration)
        {
            var testperson = _dbContext.TableTestpersons.FirstOrDefault(t => t.SybosId == registration.SybosId);

            if (testperson == null)
            {
                testperson = new TableTestperson()
                {
                    FireDepartmentBranch = registration.Station,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    SybosId = registration.SybosId,
                    Password = PasswordGenerator.GeneratePassword()
                };
                _dbContext.TableTestpersons.Add(testperson);
                _dbContext.SaveChanges();

            }

            return testperson.Id;
        }


        /// <summary>
        ///     Legt einen neuen Wissenstest an.
        /// </summary>
        /// <returns>Id des angelegten Test..</returns>
        /// <response code="200">Returniert die Id des angelegten Test..</response>
        /// <response code="401">Wenn der Benutzer nicht die richtigen Rechte für den Call hat.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpPost]
        [Route("api/knowledgetest/CreateKnowledgeTest")]
        public ActionResult CreateKnowledgeTest([FromBody] string designation)
        {
            var knowledgetest = new TableKnowledgeTest()
            {
                Designation = designation,
                EvaluatorPassword = PasswordGenerator.GeneratePassword()
            };

            try
            {
                _dbContext.TableKnowledgeTests.Add(knowledgetest);
                _dbContext.SaveChanges();

                return Ok(knowledgetest.Id);
            }
            catch (DbUpdateException)
            {
                return BadRequest($"Wissenstest {designation} konnte nicht angelegt werden, da dieser bereits in der Datenbank gespeichert ist!");
            }
            catch (Exception)
            {
                return BadRequest("{");
            }
        }
    }
}

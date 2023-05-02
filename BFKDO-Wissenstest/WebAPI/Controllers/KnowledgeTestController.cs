using Common.Model;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return BadRequest();
            }

            //var knowledgetest = _dbContext.TableKnowledgeTests
            //    .Include(t => t.Registrations).ThenInclude(t => t.KnowledgeLevel)
            //    .Include(t => t.Registrations).ThenInclude(t => t.Evaluations)
            //    .FirstOrDefault(t => t.Id == id);
            //if(knowledgetest == null)
            //{
            //    _logger.LogError($"Requested Knowledgetest with Id {id} was not found in the Database!", DateTime.Now.ToLongTimeString());
            //    return BadRequest();
            //}

            //var model = new ModelKnowledgeTestDetails()
            //{
            //    KnowledgeTestId = knowledgetest.Id,
            //    KnowledgeTestYear = knowledgetest.Designation
            //};
            //var testpersonsIds = knowledgetest.Registrations.GroupBy(t => t.TestpersonId);
            //foreach(var testpersonId in testpersonsIds)
            //{
            //    var testperson = _dbContext.TableTestpersons.FirstOrDefault(t => t.Id == testpersonId.Key);
            //    if(testperson != null!)
            //    {
            //        var results = GetResultsForTestPerson(_dbContext, knowledgetest, testperson);
            //        model.TestPersonResults.Add(new()
            //        {
            //            Name = testperson.FirstName + " " + testperson.LastName,
            //            Station = testperson.FireDepartmentBranch,
            //            Results = results
            //        });
            //    }
            //    else
            //    {
            //          _logger.LogError($"Requested Testperson with Id {id} was not found in the Database!", DateTime.Now.ToLongTimeString());
            //    }
            //}

            var model = new ModelKnowledgeTestDetails()
            {
                KnowledgeTestYear = "2022",
                KnowledgeTestId = id,
            };
            var elements = new List<ModelTestPersonResult>();
            for (int i = 0; i < 10; i++)
            {
                elements.Add(new() { Name = $"Max Super-Duper-Mustermann {i}", Station = $"FF Oberst-Donnerskirchen-Dorf {i}", Results = new() { new() { LevelName = "Stufe 1", LevelResult = "3 / 5" }, new() { LevelName = "Stufe 2", LevelResult = "5 / 5" } } });
            }

            model.TestPersonResults = elements;

            return Ok(model);
        }
    }
}

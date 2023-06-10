using Common.Model;
using Common.Models;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Identity;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Testbewerters.
    /// </summary>
    [Authorize(Policy = Identities.EvaluatorPolicyName)]
    public class EvaluatorController : ControllerBase
    {
        private readonly BfkdoDbContext _databaseContext;

        /// <summary>
        ///     Konstruktor des Evaluierungs Controllers.
        /// </summary>
        /// <param name="databaseContext"></param>
        public EvaluatorController(BfkdoDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Abruf aller Stationen.
        /// </summary>
        /// <returns>Eine Liste mit allen Stationen.</returns>
        /// <response code="200"> Abruf erfolgreich.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/evaluator/getstations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetStations()
        {
            var stations = _databaseContext.TableEvaluationCriterias.ToList();

            return Ok(stations.Select(t => new TestStationModel()
            {
                CriteriaName = t.CriteriaName,
                Id = t.Key
            }));
        }
        [HttpGet]
        [Route("api/evaluator/knowledgetest/{knowledgeTestId}/getstationdata/{stationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetStationData([FromRoute] int knowledgeTestId, [FromRoute] int stationId)
        {
            List<ModelEvaluatorGrade> stationData = new();

            //var registrations = _databaseContext.TableRegistrations
            //    .Where(registration => registration.KnowledgeTestId == knowledgeTestId).ToList();

            // Holt alle Benotungen vom aktuellen Test und von der aktuellen Kategorie.
            var grades = _databaseContext.TableEvaluations
                .Where(grade => _databaseContext.TableRegistrations
                .Any(registration => registration.KnowledgeTestId == knowledgeTestId
                 &&  registration.Id == grade.RegistrationId)
                 &&  grade.EvaluationCriteriaId == stationId
                 ).ToList();

            foreach (var grade in grades)
            {
                var data = new ModelEvaluatorGrade();

                var participant = _databaseContext.TableTestpersons
                    .Where(person => person.Id == _databaseContext.TableRegistrations.Where(registration => registration.Id == grade.RegistrationId).Single().TestpersonId).Single();

                data.ParticipantName = participant.FirstName + " " + participant.LastName;
                data.GradeId = grade.Id;
                data.Grade = grade.Evaluation;

                stationData.Add(data);
            }

            return Ok(stationData);
        }

        /// <summary>
        /// Abgabe einer Bewertung.
        /// </summary>
        /// <param name="dto">Die Bewertung</param>
        /// <returns>Ob die Abgaben erfolgreich war.</returns>
        /// <response code="200">Abgabe erfolgreich.</response>
        /// <response code="400"></response>
        /// <response code="401">Ungültiger JWT-Token.</response>t.LastName;
        [HttpPost]
        [Route("api/evaluator/submitevaluation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult SubmitEvaluation([FromBody] ModelEvaluation dto)
        {
            _databaseContext.TableEvaluations.Where(e => e.Id == dto.EvaluationId).Single().Evaluation = dto.Evaluation;
            _databaseContext.SaveChanges();
            return Ok(true);
        }

        /// <summary>
        /// Abschließen einer Bewertungsgruppe.
        /// </summary>
        /// <returns>Ob der Abschluss erfolgreich war.</returns>
        /// <response code="200">Abschluss erfolgreich.</response>
        /// <response code="400"></response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPut]
        [Route("api/evaluator/closeevaluation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CloseEvaluation()
        {
            throw new NotImplementedException();
        }
    }
}
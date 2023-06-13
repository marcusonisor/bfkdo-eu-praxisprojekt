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

        /// <summary>
        /// Ruft den Namen der angegeben StationsId ab.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>Der Name der Station.</returns>
        [HttpGet]
        [Route("api/evaluator/getstationname/{stationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetStationName([FromRoute] int stationId)
        {
            var station = _databaseContext.TableEvaluationCriterias.Where(station => station.Key == stationId).Single();

            if (station != null)
            {
                return Ok(new ModelStationName() { StationName = station.CriteriaName });
            }

            return Ok(new ModelStationName());
        }

        /// <summary>
        /// Ruf die Daten der angegebenen Station ab.
        /// </summary>
        /// <param name="knowledgeTestId"></param>
        /// <param name="stationId"></param>
        /// <returns>Die Daten der angegebenen Station.</returns>
        [HttpGet]
        [Route("api/evaluator/knowledgetest/{knowledgeTestId}/getstationdata/{stationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetStationData([FromRoute] int knowledgeTestId, [FromRoute] int stationId)
        {
            List<ModelEvaluationSet> stationData = new();

            var grades = _databaseContext.TableEvaluations
                .Where(grade => _databaseContext.TableRegistrations
                .Any(registration => registration.KnowledgeTestId == knowledgeTestId
                 &&  registration.Id == grade.RegistrationId)
                 &&  grade.EvaluationCriteriaId == stationId
                 ).ToList();

            foreach (var grade in grades)
            {
                var data = new ModelEvaluationSet();

                var participant = _databaseContext.TableTestpersons
                    .Where(person => person.Id == _databaseContext.TableRegistrations.Where(registration => registration.Id == grade.RegistrationId).Single().TestpersonId).Single();

                data.GradeId = grade.Id;
                data.Grade = grade.Evaluation;
                data.State = grade.EvaluationState;
                data.ParticipantName = participant.FirstName + " " + participant.LastName;
                data.SybosId = participant.SybosId;
                data.Branch = participant.FireDepartmentBranch;

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
        [HttpPost]
        [Route("api/evaluator/closeevaluation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CloseEvaluation([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                _databaseContext.TableEvaluations.Where(e => e.Id == id).Single().EvaluationState = Common.Enums.EnumEvaluationState.Closed;
            }

            _databaseContext.SaveChanges();

            return Ok(true);
        }
    }
}
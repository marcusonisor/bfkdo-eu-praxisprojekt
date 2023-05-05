using Common.Model;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// Abgabe einer Bewertung.
        /// </summary>
        /// <param name="dto">Die Bewertung</param>
        /// <returns>Ob die Abgaben erfolgreich war.</returns>
        /// <response code="200">Abgabe erfolgreich.</response>
        /// <response code="400"></response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPut]
        [Route("api/evaluator/submitevaluation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult SubmitEvaluation(ModelEvaluation dto)
        {
            throw new NotImplementedException();
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
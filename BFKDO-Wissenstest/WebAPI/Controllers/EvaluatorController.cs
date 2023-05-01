namespace WebAPI.Controllers
{
    using Common.Model;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Testbewerters.
    /// </summary>
    public class EvaluatorController : ControllerBase
    {
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
            throw new NotImplementedException();
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
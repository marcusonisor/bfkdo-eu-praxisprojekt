namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Testteilnehmers.
    /// </summary>
    public class ParticipantController : ControllerBase
    {
        /// <summary>
        /// Aktualisierung der Daten des Testteilnehmers
        /// </summary>
        /// <returns>Die aktualisierten Testdaten des Testeilnehmers</returns>
        /// <response code="200">Abfrage erfolgreich.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/participant/refreshdata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult RefreshData()
        {
            throw new NotImplementedException();
        }
    }
}
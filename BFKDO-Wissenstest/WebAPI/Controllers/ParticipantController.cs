using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Identity;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Testteilnehmers.
    /// </summary>
    [Authorize(Policy = Identities.ParticipantPolicyName)]
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
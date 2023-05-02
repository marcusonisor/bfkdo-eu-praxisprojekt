using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller zuständig für alle Aktionen eines authentifizierten Admins
    /// </summary>
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// Erstellung eines neuen Tests.
        /// </summary>
        /// <returns>Ob die Erstellung des neuen Tests erfolgreich war.</returns>
        /// <response code="200">Erstellung war erfolgreich.</response>
        /// <response code="400">Erstellter Test ist ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPost]
        [Route("api/admin/createtest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CreateTest(ModelTest dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bearbeitung der Daten eines Testteilnehmers.
        /// </summary>
        /// <param name="dto">Die neuen Daten des Testteilnehmers</param>
        /// <returns>Ob die Abgaben erfolgreich war.</returns>
        /// <response code="200">Bearbeitung erfolgreich.</response>
        /// <response code="400">Übermittelten Daten sind ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpPut]
        [Route("api/admin/updateparticipant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult SubmitEvaluation(ModelParticipant dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Export-PDF der Testdaten.
        /// </summary>
        /// <returns>Die Export-PDF.</returns>
        /// <response code="200">Export erfolgreich.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/admin/exportdata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult ExportData()
        {
            throw new NotImplementedException();
        }
    }
}
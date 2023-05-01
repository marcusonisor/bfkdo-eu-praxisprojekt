namespace WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Common.Model;

    /// <summary>
    /// Controller zuständig für die Authentifizierung aller Benuter.
    /// </summary>
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Login der Testteilnehmer.
        /// </summary>
        /// <param name="dto">Die Logindaten des Testteilnehmers.</param>
        /// <returns>Ob der Login eines Teilnehmers erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">SybosId oder Passwort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/participant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthParticipant(ModelParticipantAuthData dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login der Testbewerter.
        /// </summary>
        /// <param name="dto">Das Zugangspasswort des Testbewerters.</param>
        /// <returns>Ob der Login eines Testbewerters erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">Zugangspasswort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/evaluator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthEvaluator(ModelEvaluatorAuthData dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login des Admins.
        /// </summary>
        /// <param name="dto">Die Logindaten des Admins.</param>
        /// <returns>Ob der Login eines Admins erfolgreich war oder nicht.</returns>
        /// <response code="200">Login erfolgreich.</response>
        /// <response code="400">E-Mail oder Passwort ist falsch.</response>
        [HttpPost]
        [Route("api/auth/admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AuthAdmin(ModelAdminAuthData dto)
        {
            throw new NotImplementedException();
        }
    }
}
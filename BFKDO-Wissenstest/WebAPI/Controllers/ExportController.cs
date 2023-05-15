using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpDocx;
using Spire.Barcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using WebAPI.Identity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller für den Export von Zugangsdaten.
    /// </summary>
    [Authorize(Policy = Identities.AdminPolicyName)]
    public class ExportController : ControllerBase
    {
        private readonly BfkdoDbContext _databaseContext;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Konstruktor des Controllers.
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="env"></param>
        public ExportController(BfkdoDbContext databaseContext, IWebHostEnvironment env)
        {
            _databaseContext = databaseContext;
            _env = env;
        }

        /// <summary>
        /// Generiert eine .docx mit den Zugangsdaten des Bewerters für einen spezifischen Wissenstest.
        /// </summary>
        /// <returns>Eine .docx mit den Zugangsdaten des Bewerters für einen spezifischen Wissenstest.</returns>
        /// <response code="200">Erstellung war erfolgreich.</response>
        /// <response code="400">Erstellter Test ist ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/admin/export/getevaluatorcredentials/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetEvaluatorCredentials(int id)
        {
            string templatePath = _env.WebRootPath + "/ExportTemplates/evaluator-credentials-export.cs.docx";

            try
            {
                var entity = _databaseContext.TableKnowledgeTests.SingleAsync(e => e.Id == id);
                // var qr = GenerateEvaluatorQR(entity.Result.EvaluatorPassword);

                EvaluatorCredentialsExportModel model = new(entity.Result.Designation, entity.Result.EvaluatorPassword);

                var exportStream = DocumentFactory.Create(templatePath, model).Generate();

                return Ok(exportStream.ToArray());

            }
            catch (Exception e)
            {
                return null; // TODO: Return error!
            }
        }

        /// <summary>
        /// Generiert eine .docx mit den Zugangsdaten der Testteilnehmer für einen spezifischen Wissenstest.
        /// </summary>
        /// <returns>Eine .docx mit den Zugangsdaten der Testteilnehmer für einen spezifischen Wissenstest.</returns>
        /// <response code="200">Erstellung war erfolgreich.</response>
        /// <response code="400">Erstellter Test ist ungültig.</response>
        /// <response code="401">Ungültiger JWT-Token.</response>
        [HttpGet]
        [Route("api/admin/export/getparticipantscredentials/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetParticipantsCredentials(int id)
        {
            string templatePath = _env.WebRootPath + "/ExportTemplates/participant-credentials-export.cs.docx";

            try
            {
                List<ParticipantCredentialsExportModel> credentials = new();

                var participants = _databaseContext.TableTestpersons.Where(e => e.Registrations.Any(e => e.KnowledgeTestId == id));

                foreach (var participant in participants)
                {
                    credentials.Add(new ParticipantCredentialsExportModel(participant.SybosId.ToString(), participant.Password));
                }

                ParticipantsCredentialsExportModel model = new(credentials);

                var exportStream = DocumentFactory.Create(templatePath, model).Generate();

                return Ok(exportStream.ToArray());

            }
            catch (Exception e)
            {
                return null; // TODO: Return error!
            }
        }
    }
}
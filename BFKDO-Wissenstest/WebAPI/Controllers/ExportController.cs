using Common.Model;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpDocx;
using System.IO;
using WebAPI.Identity;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize(Policy = Identities.AdminPolicyName)]
    public class ExportController : ControllerBase
    {
        private readonly BfkdoDbContext _databaseContext;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        ///     Konstruktor des Evaluierungs Controllers.
        /// </summary>
        /// <param name="databaseContext"></param>
        public ExportController(BfkdoDbContext databaseContext, IWebHostEnvironment env)
        {
            _databaseContext = databaseContext;
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Ob die Erstellung des neuen Tests erfolgreich war.</returns>
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
            string templatePath = _env.WebRootPath + "/ExportTemplates/evaluator-export.cs.docx";

            try
            {
                var entity = _databaseContext.TableKnowledgeTests.SingleAsync(e => e.Id == id);


                EvaluatorCredentialsExportModel model = new(entity.Result.Designation, entity.Result.EvaluatorPassword);

                var exportStream = DocumentFactory.Create(templatePath, model).Generate();

                return Ok(exportStream.ToArray());

            }
            catch (Exception ex)
            {
                return null; // TODO: Return error!
            }
        }
    }
}
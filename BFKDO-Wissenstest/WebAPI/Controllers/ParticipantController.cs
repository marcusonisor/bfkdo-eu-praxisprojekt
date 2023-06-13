using Common.Models;
using Database;
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
        private readonly BfkdoDbContext _dbContext;
        private readonly ILogger<KnowledgeTestController> _logger;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public ParticipantController(BfkdoDbContext dbContext, ILogger<KnowledgeTestController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Ruft die Dashboarddaten ab.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Die Dashboarddaten.</returns>
        [HttpGet]
        [Route("api/participant/{id}/getdashboarddata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetDashboardData(int id)
        {
            List<ModelExaminationLevel> dashboardData = new();

            var levels = _dbContext.TableKnowledgeLevels.ToList();
            var criterias = _dbContext.TableEvaluationCriterias.ToList();
            var registrations = _dbContext.TableRegistrations.Where(e => e.TestpersonId == id).ToList();

            var newestRegistrations = registrations.GroupBy(e => e.KnowledgeLevelId)
                                                   .Select(group => group
                                                   .OrderByDescending(e => e.Id).First()).ToList();

            foreach (var level in levels)
            {
                var modelLevel = new ModelExaminationLevel();

                modelLevel.Id = level.Id;
                modelLevel.Name = level.Description;

                if (newestRegistrations.Any(e => e.KnowledgeLevelId == level.Id))
                    modelLevel.Disabled = false;

                dashboardData.Add(modelLevel);
            }

            foreach (var registration in newestRegistrations)
            {
                var grades = _dbContext.TableEvaluations.Where(e => e.RegistrationId == registration.Id).ToList();
                
                foreach (var grade in grades)
                {
                    var modelResult = new ModelExaminationResult();
                    modelResult.Name = criterias.Where(e => e.Key == grade.EvaluationCriteriaId).FirstOrDefault()!.CriteriaName;
                    modelResult.Evaluation = grade.Evaluation;

                    dashboardData.Where(e => e.Id == registration.KnowledgeLevelId).FirstOrDefault()!.Results.Add(modelResult);
                }
            }

            return Ok(dashboardData);
        }

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
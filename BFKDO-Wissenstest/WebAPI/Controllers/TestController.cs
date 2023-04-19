using Database;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     Test Controller.
    /// </summary>
    [Controller]
    public class TestController : ControllerBase
    {
        private readonly BfkdoDbContext _databaseContext;

        /// <summary>
        ///     Konstruktor des Testcontrollers.
        /// </summary>
        /// <param name="db"></param>
        public TestController(BfkdoDbContext db)
        {
            _databaseContext = db;
        }

        /// <summary>
        ///     Schnittstelle zum Erstellen der Datenbank.
        /// </summary>
        /// <returns>Ok, wenn Datenbank erstellt wurde</returns>
        [Route("/test/createdatabase")]
        [HttpGet]
        public async Task<ActionResult> CreateDatabase()
        {
            await _databaseContext.CreateInitialFillUp();
            return Ok();
        }
    }
}

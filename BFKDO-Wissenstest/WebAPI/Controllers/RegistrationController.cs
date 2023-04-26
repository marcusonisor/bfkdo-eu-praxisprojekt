using Microsoft.AspNetCore.Mvc;
using Common.Model.CSVModels;
using Common.Helper;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     Controller für Registrierungs-Methoden.
    /// </summary>
    public class RegistrationController : ControllerBase
    {
        /// <summary>
        ///     Lesen der Registrierungen aus der CSV.
        /// </summary>
        /// <param name="bytes">Ausgelesene Bytes der Datei.</param>
        /// <returns>Ob Teilnehmer ausgelesen werden konnten.</returns>
        /// <response code="200">Returniert, dass Einträge aus der CSV ausgelesen werden konnten.</response>
        /// <response code="400">Wenn aus dem gebotetenen CSV Byte Array keine Einträge erstellt werden konnten.</response>
        /// <response code="401">Wenn der Benutzer nicht die richtigen Rechte für den Call hat.</response>
        [Route("api/Registration/ReadRegistrationsFromCsv")]
        [HttpPost]
        public ActionResult ReadRegistrationsFromCsv([FromBody] byte[] bytes)
        {
            var data = CsvHandlingHelper.GetDataFromCsvByteArray<CsvRegistrationModel>(bytes);
            if (!data.Any())
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

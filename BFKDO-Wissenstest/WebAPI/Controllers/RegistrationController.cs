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
        [Route("api/Registration/ReadRegistrationsFromCsv")]
        [HttpPost]
        public ActionResult ReadRegistrationsFromCsv([FromBody] byte[] bytes)
        {
            var data = CsvHandlingHelper.GetDataFromCsvByteArray<CsvRegistrationModel>(bytes);
            return Ok(data.Any());
        }
    }
}

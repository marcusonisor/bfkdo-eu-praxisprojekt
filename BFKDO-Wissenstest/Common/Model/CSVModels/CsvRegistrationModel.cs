using CsvHelper.Configuration.Attributes;

namespace Common.Model.CSVModels
{
    /// <summary>
    ///     Registrierungsmodel für Csv-Auslesen.
    /// </summary>
    public class CsvRegistrationModel
    {
        [Name("Nachname")]
        public string LastName { get; set; } = string.Empty;

        [Name("Vorname")]
        public string FirstName { get; set; } = string.Empty;

        [Name("Geburtsdatum")]
        [Format("dd.MM.yyyy")]
        public DateTime Birthday { get; set; }

        [Name("Wertungsklasse")]
        public string RegistratedLevel { get; set; } = string.Empty;

        [Name("Feuerwehr")]
        public string Station { get; set; } = string.Empty;

        [Name("ADadrnr")]
        public long SybosId { get; set; }
    }
}

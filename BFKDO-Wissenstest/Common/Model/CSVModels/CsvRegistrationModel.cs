using CsvHelper.Configuration.Attributes;

namespace Common.Model.CSVModels
{
    /// <summary>
    ///     Registrierungsmodel für Csv-Auslesen.
    /// </summary>
    public class CsvRegistrationModel
    {
        /// <summary>
        ///     Nachname.
        /// </summary>
        [Name("Nachname")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        ///     Vorname.
        /// </summary>
        [Name("Vorname")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        ///     Geburtsdatum.
        /// </summary>
        //[Name("Geburtsdatum")]
        //[Format("dd.MM.yyyy")]
        //public DateTime Birthday { get; set; }

        /// <summary>
        ///     Wertungsklasse.
        /// </summary>
        [Name("Wertungsklasse")]
        public string RegistratedLevel { get; set; } = string.Empty;

        /// <summary>
        ///     Angemeldete Feuerwehr.
        /// </summary>
        [Name("Feuerwehr")]
        public string Station { get; set; } = string.Empty;

        /// <summary>
        ///     Sybos Id.
        /// </summary>
        [Name("ADadrnr")]
        public long SybosId { get; set; }
    }
}

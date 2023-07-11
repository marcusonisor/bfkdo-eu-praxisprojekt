using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.CSVModels
{
    /// <summary>
    ///     CSV Result Model.
    /// </summary>
    public class CsvResultModel
    {
        /// <summary>
        ///     Sybos Id.
        /// </summary>
        [Name("Sybos Id")]
        public string SybosId { get; set; } = string.Empty;

        /// <summary>
        ///     Vorname.
        /// </summary>
        [Name("Vorname")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        ///     Nachname.
        /// </summary>
        [Name("Nachname")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        ///     Geschafft.
        /// </summary>
        [Name("Bestanden?")]
        public string Passed { get; set; } = string.Empty;

        /// <summary>
        ///     Ergebnis.
        /// </summary>
        [Name("Ergebnis")]
        public string Result { get; set; } = string.Empty;
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Tables
{
    /// <summary>
    ///     Datenbank-Modell für die Wissenstests.
    /// </summary>
    [Index(nameof(Designation), IsUnique = true)]
    [Table("Tbl_KnowledgeTest")]
    public class TableKnowledgeTest
    {
        /// <summary>
        ///     Id des Wissenstest.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Bezeichnung des Wissenstest.
        /// </summary>
        public string Designation { get; set; } = string.Empty;

        /// <summary>
        ///     Passwort für die Bewerter.
        /// </summary>
        public string EvaluatorPassword { get; set; } = string.Empty;

        /// <summary>
        ///     Registrierungen für den Wissenstest.
        /// </summary>
        public virtual ICollection<TableRegistration> Registrations { get; set; } = new List<TableRegistration>();
    }
}

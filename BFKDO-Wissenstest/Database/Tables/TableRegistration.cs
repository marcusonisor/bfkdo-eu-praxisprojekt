using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Tables
{
    /// <summary>
    ///     Datenbank-Modell für die Registrierung
    /// </summary>
    [Table("Tbl_Registration")]
    public class TableRegistration
    {
        /// <summary>
        ///     Id der Registrierung.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Id des Wissenstest.
        /// </summary>
        public int? KnowledgeTestId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz des Wissenstest.
        /// </summary>
        [ForeignKey(nameof(KnowledgeTestId))]
        public virtual TableKnowledgeTest KnowledgeTest { get; set; } = null!;

        /// <summary>
        ///     Id der Testperson.
        /// </summary>
        public int? TestpersonId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz der Testperson.
        /// </summary>
        [ForeignKey(nameof(TestpersonId))]
        public virtual TableTestperson Testperson { get; set; } = null!;

        /// <summary>
        ///     Id der Wissenstufe.
        /// </summary>
        public int? KnowledgeLevelId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz der Wissensstufe.
        /// </summary>
        [ForeignKey(nameof(KnowledgeLevelId))]
        public virtual TableKnowledgeLevel KnowledgeLevel { get; set; } = null!;

        /// <summary>
        ///     Bewertungen.
        /// </summary>
        public virtual ICollection<TableEvaluation> Evaluations { get; set; } = new List<TableEvaluation>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Tables
{
    /// <summary>
    ///     Datenbank-Modell für Bewertungskriterien.
    /// </summary>
    [Table("Tbl_EvaluationCriteria")]
    public class TableEvaluationCriteria
    {
        /// <summary>
        ///     Id des Bewertungskriteriums.
        /// </summary>
        [Key]
        public int Key { get; set; }

        /// <summary>
        ///     Id des Wissensstufe.
        /// </summary>
        public int? KnowledgeLevelId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz der Wissensstufe.
        /// </summary>
        [ForeignKey(nameof(KnowledgeLevelId))]
        public virtual TableKnowledgeLevel KnowledgeLevel { get; set; } = null!;

        /// <summary>
        ///     Id des Wissenskapitel.
        /// </summary>
        public int? KnowledgeSectionId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz des Wissenskapitel.
        /// </summary>
        [ForeignKey(nameof(KnowledgeSectionId))]
        public virtual TableKnowledgeSection KnowledgeSection { get; set; } = null!;
    }
}

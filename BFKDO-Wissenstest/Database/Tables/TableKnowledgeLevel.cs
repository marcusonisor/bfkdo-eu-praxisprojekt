using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Tables
{
    /// <summary>
    ///     Datenbank-Modell der Wissenstufe.
    /// </summary>
    [Table("Tbl_KnowledgeLevel")]
    public class TableKnowledgeLevel
    {
        /// <summary>
        ///     Id der Wissensstufe.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Bezeichnung der Wissensstufe.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Anmeldungen für die Stufe.
        /// </summary>
        public virtual ICollection<TableRegistration> Registrations { get; set; } = new List<TableRegistration>();
    
        /// <summary>
        ///     Bewertungskriterien.
        /// </summary>
        public virtual ICollection<TableEvaluationCriteria> EvaluationCriterias { get; set; } = new List<TableEvaluationCriteria>();
    }
}

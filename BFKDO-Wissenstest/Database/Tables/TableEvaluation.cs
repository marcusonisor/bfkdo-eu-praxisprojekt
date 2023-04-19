using Common.Enums;
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
    ///     Datenbankmodell der Bewertung.
    /// </summary>
    [Table("Tbl_Evaluation")]
    public class TableEvaluation
    {
        /// <summary>
        ///     Id der Bewertung.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Id der Registrierung.
        /// </summary>
        public int? RegistrationId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz der Registrierung.
        /// </summary>
        [ForeignKey(nameof(RegistrationId))]
        public virtual TableRegistration Registration { get; set; } = null!;

        /// <summary>
        ///     Id des Bewertungskriterium.
        /// </summary>
        public int? EvaluationCriteriaId { get; set; }

        /// <summary>
        ///     Virtuelle Instanz des Bewertungskriterium.
        /// </summary>
        public virtual TableEvaluationCriteria EvaluationCriteria { get; set; } = null!;

        /// <summary>
        ///     Bewertung.
        /// </summary>
        public EnumEvaluation Evaluation { get; set; }

        /// <summary>
        ///     Bewertungsstatus.
        /// </summary>
        public EnumEvaluationState EvaluationState { get; set; }
    }
}

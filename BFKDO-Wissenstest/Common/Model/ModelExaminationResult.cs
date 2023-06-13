using Common.Enums;

namespace Common.Models
{
    /// <summary>
    /// DTO für das Ergebnis eines Wissenstests.
    /// </summary>
    public class ModelExaminationResult
    {
        /// <summary>
        /// Name des Ergebnisses.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Note des Ergebnisses.
        /// </summary>
        public EnumEvaluation Evaluation { get; set; } = EnumEvaluation.Ungraded;
    }
}

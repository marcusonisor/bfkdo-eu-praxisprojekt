using Common.Enums;

namespace Common.Model
{
    /// <summary>
    /// DTO der Auswertung.
    /// </summary>
    public class ModelEvaluationSet
    {

        /// <summary>
        /// Id der Auswertung.
        /// </summary>
        public int GradeId { get; set; }

        /// <summary>
        /// Note der Auswertung.
        /// </summary>
        public EnumEvaluation Grade { get; set; } = EnumEvaluation.Ungraded;

        /// <summary>
        /// Zustand der Auswertung.
        /// </summary>
        public EnumEvaluationState State { get; set; } = EnumEvaluationState.Open;

        /// <summary>
        /// Name des Teilnehmers.
        /// </summary>
        public string ParticipantName { get; set; } = string.Empty;

        /// <summary>
        /// SybosId des Teilnehmers.
        /// </summary>
        public int SybosId { get; set; }

        /// <summary>
        /// Dienststelle des Teilnehmers.
        /// </summary>
        public string Branch { get; set; } = string.Empty;
    }
}

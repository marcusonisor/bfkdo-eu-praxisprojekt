using Common.Enums;

namespace Common.Model
{
    /// <summary>
    /// DTO der Auswertung.
    /// </summary>
    public class ModelEvaluation
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="evaluationId"></param>
        /// <param name="evaluation"></param>
        public ModelEvaluation(int evaluationId, EnumEvaluation evaluation)
        {
            EvaluationId = evaluationId;
            Evaluation = evaluation;
        }

        /// <summary>
        /// ID des Bewertungskriterium.
        /// </summary>
        public int EvaluationId { get; }

        /// <summary>
        /// Die Bewertung.
        /// </summary>
        public EnumEvaluation Evaluation { get; }
    }
}
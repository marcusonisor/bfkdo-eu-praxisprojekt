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
        /// <param name="registrationId"></param>
        /// <param name="evaluationCriteriaId"></param>
        /// <param name="evaluation"></param>
        public ModelEvaluation(int registrationId, int evaluationCriteriaId, EnumEvaluation evaluation)
        {
            RegistrationId = registrationId;
            EvaluationCriteriaId = evaluationCriteriaId;
            Evaluation = evaluation;
        }

        /// <summary>
        /// ID der Registrierung.
        /// </summary>
        public int RegistrationId { get; }

        /// <summary>
        /// ID des Bewertungskriterium.
        /// </summary>
        public int EvaluationCriteriaId { get; }

        /// <summary>
        /// Die Bewertung.
        /// </summary>
        public EnumEvaluation Evaluation { get; }
    }
}
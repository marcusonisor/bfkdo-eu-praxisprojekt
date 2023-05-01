namespace Common.Model
{
    using Common.Enums;

    public class ModelEvaluation
    {
        public ModelEvaluation(int registrationId, int evaluationCriteriaId, EnumEvaluation evaluation)
        {
            RegistrationId = registrationId;
            EvaluationCriteriaId = evaluationCriteriaId;
            Evaluation = evaluation;
        }

        public int RegistrationId { get; }

        public int EvaluationCriteriaId { get; }

        public EnumEvaluation Evaluation { get; }
    }
}
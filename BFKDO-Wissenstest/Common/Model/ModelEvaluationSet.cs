using Common.Enums;

namespace Common.Model
{
    public class ModelEvaluationSet
    {
        public int GradeId { get; set; }

        public EnumEvaluation Grade { get; set; } = EnumEvaluation.Ungraded;

        public EnumEvaluationState State { get; set; } = EnumEvaluationState.Open;

        public string ParticipantName { get; set; } = string.Empty;

        public int SybosId { get; set; }

        public string Branch { get; set; } = string.Empty;
    }
}

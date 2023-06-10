using Common.Enums;

namespace Common.Model
{
    public class ModelEvaluatorGrade // Umbennen
    {
        public int GradeId { get; set; }

        public EnumEvaluation Grade { get; set; } = EnumEvaluation.Ungraded;

        public string ParticipantName { get; set; } = string.Empty;
    }
}

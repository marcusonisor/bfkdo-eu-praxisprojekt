using Common.Enums;

namespace Common.Models
{
    public class ModelExaminationResult
    {

        public string Name { get; set; } = string.Empty;

        public EnumEvaluation Evaluation { get; set; } = EnumEvaluation.Ungraded;

    }
}

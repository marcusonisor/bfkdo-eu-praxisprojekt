namespace Common.Models
{
    public class ModelExaminationLevel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Disabled { get; set; } = true;

        public List<ModelExaminationResult> Results { get; set; } = new();
    }
}

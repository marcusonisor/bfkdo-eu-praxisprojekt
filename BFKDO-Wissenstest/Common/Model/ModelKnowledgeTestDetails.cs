namespace Common.Model
{
    public class ModelKnowledgeTestDetails
    {
        public string Name { get; set; } = string.Empty;

        public string Station { get; set; } = string.Empty;

        public List<ModelKnowledgeLevelResult> Results { get; set; } = new(); 
    }

    public class ModelKnowledgeLevelResult
    {
        public string LevelName { get; set; } = string.Empty;

        public string LevelResult { get; set; } = string.Empty;
    }
}

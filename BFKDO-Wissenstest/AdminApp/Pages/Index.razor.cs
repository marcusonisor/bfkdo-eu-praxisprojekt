using Common.Model;

namespace AdminApp.Pages
{
    public partial class Index
    {
        public List<ModelKnowledgeTestDetails> Elements { get; } = new List<ModelKnowledgeTestDetails>();
        protected override void OnAfterRender(bool firstRender)
        {
            for (int i = 0; i < 10; i++)
            {
                Elements.Add(new() { Name = $"Max Mustermann {i}", Station = $"FF FFFFFFF{i}", Results = new() { new() { LevelName = "Stufe 1", LevelResult = "3/5" }, new() { LevelName = "Stufe 2", LevelResult = "5/5" } } });
            }

            base.OnAfterRender(firstRender);
        }
    }
}
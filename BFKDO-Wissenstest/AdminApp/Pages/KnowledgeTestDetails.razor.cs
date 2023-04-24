using Common.Model;

namespace AdminApp.Pages
{
    public partial class KnowledgeTestDetails
    {
        /// <summary>
        ///     Liste an Testelementen.
        /// </summary>
        public List<ModelKnowledgeTestDetails> Elements { get; } = new List<ModelKnowledgeTestDetails>();
        /// <summary>
        ///     On After Render Callback.
        /// </summary>
        /// <param name = "firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            for (int i = 0; i < 10; i++)
            {
                Elements.Add(new() { Name = $"Max Super-Duper-Mustermann {i}", Station = $"FF Oberst-Donnerskirchen-Dorf {i}", Results = new() { new() { LevelName = "Stufe 1", LevelResult = "3 / 5" }, new() { LevelName = "Stufe 2", LevelResult = "5 / 5" } } });
            }

            base.OnAfterRender(firstRender);
        }
    }
}
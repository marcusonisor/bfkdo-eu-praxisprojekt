using AdminApp.Services;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Details einer Testung.
    /// </summary>
    public partial class KnowledgeTestDetails
    {
        /// <summary>
        ///     Suchstring für Filter.
        /// </summary>
        private string _searchString = string.Empty;

        /// <summary>
        ///     Model für die View.
        /// </summary>
        public ModelKnowledgeTestDetails Model { get; set; } = new();

        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Id der Testung.
        /// </summary>
        [Parameter]
        public int KnowledgeTestId { get; set; }

        /// <summary>
        ///     On After Render Callback.
        /// </summary>
        /// <param name = "firstRender"></param>
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var result = await Service.GetKnowledgeTestDetails(KnowledgeTestId);
                if (result.WasSuccess)
                {
                    Model = result.Result;
                    StateHasChanged();
                }
            }

            base.OnAfterRender(firstRender);
        }

        private Func<ModelTestPersonResult, bool> _quickSearch => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.Station.Contains(_searchString))
                return true;

            if (x.Name.Contains(_searchString))
                return true;

            if (x.Results.Any(t => t.LevelName.Contains(_searchString)))
                return true;

            return false;
        };
    }
}
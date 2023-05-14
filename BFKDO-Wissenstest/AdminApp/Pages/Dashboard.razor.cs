using AdminApp.Services;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace AdminApp.Pages
{
    /// <summary>
    /// Dashboard
    /// </summary>
    public partial class Dashboard
    {
        /// <summary>
        /// Navigation Manager.
        /// </summary>
        [Inject]
        public NavigationManager Nav { get; set; } = null!;

        /// <summary>
        /// Kommunikations Service.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        private List<ModelKnowledgeTest> _knowledgetests = new();

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var response = await Service.GetKnowledgeTests();
            _knowledgetests = response.Result;
        }

        private void NavigateToDetails(int id)
        {
            //  TODO ausgewählte ID statt 4.
            Nav?.NavigateTo($"/knowledgetestdetails/{id}");
        }

    }
}
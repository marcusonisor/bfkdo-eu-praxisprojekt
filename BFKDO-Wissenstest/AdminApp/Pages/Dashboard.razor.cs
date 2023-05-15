using AdminApp.Services;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Dashboard
    /// </summary>
    public partial class Dashboard
    {
        /// <summary>
        ///     Navigation Manager.
        /// </summary>
        [Inject]
        public NavigationManager Nav { get; set; } = null!;

        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public ExportService ExportService { get; set; } = null!;

        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Liste der Wissenstest.
        /// </summary>
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

        /// <summary>
        ///     Methode zur Weiterleitung auf die Detailsseite des Wissenstest.
        /// </summary>
        /// <param name="id"></param>
        private void NavigateToDetails(int id)
        {
            Nav?.NavigateTo($"/knowledgetestdetails/{id}");
        }

        private async Task ExportEvaluatorCredentials(int knowledgetestId)
        {
            await ExportService.DownloadEvaluatorCredentials(knowledgetestId);
        }

        private async Task ExportParticipantsCredentials(int knowledgetestId)
        {
            await ExportService.DownloadParticipantsCredentials(knowledgetestId);
        }

    }
}
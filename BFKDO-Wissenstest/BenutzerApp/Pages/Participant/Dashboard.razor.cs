using BenutzerApp.Services;
using Common.Models;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Pages.Participant
{
    /// <summary>
    /// Partielle Klasse für das Dashboard.
    /// </summary>
    public partial class Dashboard
    {
        /// <summary>
        /// Die Dashboard Daten.
        /// </summary>
        public List<ModelExaminationLevel> _data = new();

        /// <summary>
        /// Der ParticipantService.
        /// </summary>
        [Inject]
        public ParticipantService ParticipantService { get; set; } = null!;

        /// <summary>
        /// Der AuthenticationStateService.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        /// <summary>
        /// Der NavigationManager.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        /// <summary>
        /// Die Initialisierungmethode.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            UpdateData();
        }

        /// <summary>
        /// Aktualisiert die Daten des Dashboards.
        /// </summary>
        private async void UpdateData()
        {
            var contextId = AuthenticationStateService.GetContextId();
            var response = await ParticipantService.GetDashboardData(contextId);
            _data = response.Result;
            StateHasChanged();
        }
    }
}

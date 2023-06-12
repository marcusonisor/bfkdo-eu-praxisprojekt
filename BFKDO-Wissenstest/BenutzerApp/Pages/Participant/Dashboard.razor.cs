using BenutzerApp.Services;
using Common.Models;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Pages.Participant
{
    public partial class Dashboard
    {
        public List<ModelExaminationLevel> _data = new();

        [Inject]
        public ParticipantService ParticipantService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            //if (AuthenticationStateService.IsLoggedIn())
            //{
            //    NavigationManager.NavigateTo("/dashboard");
            //}

            base.OnInitialized();

            UpdateData();
            //}
        }

        private async void UpdateData()
        {
            var response = await ParticipantService.GetDashboardData(7);
            _data = response.Result;
            StateHasChanged();
        }
    }
}

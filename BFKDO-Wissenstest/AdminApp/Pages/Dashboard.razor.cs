using AdminApp.Services;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

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
        ///     Authentifizierungs Service.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        /// <summary>
        /// Kommunikations Service.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Snackbar Service.
        /// </summary>
        [Inject]
        public ISnackbar SnackbarService { get; set; } = null!;

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            if (!AuthenticationStateService.HasJwtAuthentication())
            {
                Nav.NavigateTo("/");
                SnackbarService.Add("Bitte loggen Sie sich zuerst ein!", Severity.Error);
            }

            base.OnInitialized();
        }

        private void NavigateToDetails()
        {
            Nav?.NavigateTo("/knowledgetestdetails/4");
        }
    }
}
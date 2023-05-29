using BenutzerApp.Services;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BenutzerApp.Pages.Auth
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class AuthEvaluator
    {
        /// <summary>
        ///     Initialisierungmethode.
        /// </summary>
        protected override void OnInitialized()
        {
            //if (AuthenticationStateService.IsLoggedIn())
            //{
            //    NavigationManager.NavigateTo("/dashboard");
            //}

            base.OnInitialized();
        }


        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;


        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;


        public bool Processing { get; set; } = false;


        public async void Login()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                Processing = true;
                var response = await AuthService.AuthEvaluator(new ModelEvaluatorAuthData(Password));
                Processing = false;

                StateHasChanged();

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    NavigationManager.NavigateTo("/stationchange");
                }
                else
                {
                    MudSnackbar.Add("Ungültiges Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
                }

            }
            else
            {
                MudSnackbar.Add("Ungültiges Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
            }
        }

        private async void QRScanned(string args)
        {
            try
            {
                Processing = true;
                var response = await AuthService.AuthEvaluator(new ModelEvaluatorAuthData(args));
                Processing = false;

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    NavigationManager.NavigateTo("/stationchange");
                }
                else
                {
                    MudSnackbar.Add("Ungültiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
                }
            }
            catch (Exception e)
            {
                Processing = false;
                MudSnackbar.Add("Ungültiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
            }
        }

        /// <summary>
        ///     Properties für die Passwortanzeige-Funktion.
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InputType PasswordInput { get; set; } = InputType.Password;
        /// <summary>
        /// 
        /// </summary>
        public string PasswordInputIcon { get; set; } = Icons.Material.Filled.VisibilityOff;

        /// <summary>
        ///     Methode zum Anzeigen oder Verbergen des Passworts.
        /// </summary>
        private void ShowPassword()
        {
            if (IsShow)
            {
                IsShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                IsShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
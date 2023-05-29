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
    public partial class AuthParticipant
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
        /// SybosID
        /// </summary>
        public string SybosID { get; set; } = string.Empty;

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;


        public bool Processing { get; set; } = false;


        public async void Login()
        {
            if (!string.IsNullOrEmpty(SybosID) && !string.IsNullOrEmpty(Password))
            {
                Processing = true;
                var response = await AuthService.AuthParticipant(new ModelParticipantAuthData(int.Parse(SybosID), Password));
                Processing = false;

                StateHasChanged();

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    MudSnackbar.Add("SUCK", Severity.Success);
                    //NavigationManager.NavigateTo("/");
                }
                else
                {
                    MudSnackbar.Add("Ungültige SybosID oder Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
                }

            }
            else
            {
                MudSnackbar.Add("Ungültige SybosID oder Passwort! Bitte versuchen Sie es erneut!", Severity.Error);
            }
        }

        private async void QRScanned(string args)
        {
            var credentials = args.Split();
            try
            {
                Processing = true;
                var response = await AuthService.AuthParticipant(new ModelParticipantAuthData(int.Parse(credentials[0]), credentials[1]));
                Processing = false;

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    MudSnackbar.Add("SUCK", Severity.Success);
                }
                else
                {
                    MudSnackbar.Add("Ungültiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
                }
            }
            catch (Exception e)
            {
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
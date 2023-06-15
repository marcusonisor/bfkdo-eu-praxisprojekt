using BenutzerApp.Services;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BenutzerApp.Pages.Login
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class EvaluatorLogin
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

        /// <summary>
        /// Service für die Verwaltung und Überwachung des Authorisierungsstatus.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthenticationStateService { get; set; } = null!;

        /// <summary>
        /// Service für die Authorisierung.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        /// <summary>
        ///     Passwort.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Boolean der Auskunft darüber gibt, ob die Applikation gerade eine Verarbeitung durchführt.
        /// </summary>
        public bool Processing { get; set; } = false;

        /// <summary>
        /// Loginmethode.
        /// </summary>
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
                    MudSnackbar.Add("Login erfolgreich!", Severity.Success);
                    await AuthService.SetEvaluatorContextId(Password);
                    NavigationManager.NavigateTo("/evaluator/dashboard");
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

        /// <summary>
        /// Methode für die Weiterverarbeitung eines gescannten QR Codes.
        /// </summary>
        /// <param name="args"></param>
        private async void QRScanned(string args)
        {
            try
            {
                Processing = true;
                var response = await AuthService.AuthEvaluator(new ModelEvaluatorAuthData(args));
                Processing = false;

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    MudSnackbar.Add("Login erfolgreich!", Severity.Success);
                    await AuthService.SetEvaluatorContextId(Password);
                    NavigationManager.NavigateTo("/evaluator/dashboard");
                }
                else
                {
                    MudSnackbar.Add("Ungültiger QR-Code! Bitte versuchen Sie es erneut!", Severity.Error);
                }
            }
            catch (Exception)
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
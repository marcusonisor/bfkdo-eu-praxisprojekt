using Microsoft.AspNetCore.Components;
using Common.Model;
using AdminApp.Services;
using MudBlazor;
using Common.Enums;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Login Komponente.
    /// </summary>
    public partial class Login
    {
        /// <summary>
        ///     Authentication Service.
        /// </summary>
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Login des Users.
        /// </summary>
        ///
        public async void LoginUser()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                var response = await AuthService.Auth(new ModelAdminAuthData(Email, Password));

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    // Redirect to the main application or perform other actions
                    NavigationManager.NavigateTo("/dashboard");
                }

                else
                {
                    // Show error message
                    MudSnackbar.Add("Invalid email or password. Please try again.", Severity.Error);
                }
            }
        }

            bool isShow;
            InputType PasswordInput = InputType.Password;
            string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

            void ButtonTestclick()
            {
                if(isShow)
                {
                    isShow = false;
                    PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                    PasswordInput = InputType.Password;
                }
                else
                {
                    isShow = true;
                    PasswordInputIcon = Icons.Material.Filled.Visibility;
                    PasswordInput = InputType.Text;
                }
            }
        }
    } 
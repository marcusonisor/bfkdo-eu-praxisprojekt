using Microsoft.AspNetCore.Components;
using Common.Model;
using AdminApp.Services;

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

        /// <summary>
        ///     Login des Users.
        /// </summary>
        public async void LoginUser()
        {
            var response = await AuthService.Auth(new ModelAdminAuthData("admin@bfkdo.at", "default"));
        }
    }
}
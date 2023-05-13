using Common.Services;
using Microsoft.AspNetCore.Components;

namespace Common.Helper
{
    /// <summary>
    ///     Razor Page Helper.
    /// </summary>
    public class AuthenticationRazorPage : ComponentBase
    {
        /// <summary>
        ///     Authentication State Service.
        /// </summary>
        [Inject]
        public AuthenticationStateService AuthStateService { get; set; } = null!;

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        protected override void OnInitialized()
        {
            AuthStateService.CheckJwtAuthentication();
            base.OnInitialized();
        }

        /// <summary>
        ///     Asynchrone Initialisierung.
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            OnInitialized();
            return base.OnInitializedAsync();
        }
    }
}

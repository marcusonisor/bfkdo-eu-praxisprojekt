using Blazored.LocalStorage;
using Common.Model;
using Common.Services;
using Common.Enums;
using Microsoft.AspNetCore.Components;

namespace AdminApp.Services
{
    /// <summary>
    ///     Authentication Service.
    /// </summary>
    public class AuthService : BaseService
    {
        /// <summary>
        /// Konstruktor des Services.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="authStateService">Authentication Status Service.</param>
        /// <param name="navigationManager">Navigation.</param>
        public AuthService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
        {
        }

        /// <summary>
        /// Retourniert eine Liste mit allen Stationen.
        /// </summary>
        /// <returns>Liste mit allen Stationen.</returns>
        public async Task<HttpRequestResult<TokenModel>> Auth(ModelAdminAuthData authData)
        {
            var result = await PostToApi<ModelAdminAuthData, TokenModel>("/api/auth/admin", authData);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                _authStateService.AddJwtToken(result.Result.Token);
            }

            return result;
        }
    }
}
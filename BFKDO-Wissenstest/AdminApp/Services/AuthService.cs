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
        /// Konstruktor des StationsService.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="storageService">SpeicherService.</param>
        /// <param name="navigationManager">Navigation.</param>
        public AuthService(HttpClient client, ISyncLocalStorageService storageService, NavigationManager navigationManager) : base(client,storageService,navigationManager)
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
                AddJwtToken(result.Result.Token);
            }

            return result;
        }
    }
}
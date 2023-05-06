using Blazored.LocalStorage;
using Common.Model;
using Common.Services;

namespace AdminApp.Services
{
    public class AuthService : BaseService
    {
        /// <summary>
        /// Konstruktor des StationsService.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="storageService">SpeicherService.</param>
        public AuthService(HttpClient client, ISyncLocalStorageService storageService) : base(client,storageService)
        {
        }

        /// <summary>
        /// Retourniert eine Liste mit allen Stationen.
        /// </summary>
        /// <returns>Liste mit allen Stationen.</returns>
        public async Task<HttpRequestResult<TokenModel>> Auth(ModelAdminAuthData authData)
        {
            var result = await PostToApi<ModelAdminAuthData, TokenModel>("/api/auth/admin", authData);

            if (result.WasSuccess)
            {
                AddJwtToken(result.Result.Token);
            }

            return result;
        }
    }
}
using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Services;

namespace BenutzerApp.Services
{
    /// <summary>
    /// Service für Stationen.
    /// </summary>
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
        public async Task<HttpRequestResult<TokenModel>> AuthEvaluator(ModelEvaluatorAuthData authData)
        {
            var result = await PostToApi<ModelEvaluatorAuthData, TokenModel>("/api/auth/evaluator", authData);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                AddJwtToken(result.Result.Token);
            }

            return result;
        }

        /// <summary>
        /// Retourniert eine Liste mit allen Stationen.
        /// </summary>
        /// <returns>Liste mit allen Stationen.</returns>
        public async Task<HttpRequestResult<TokenModel>> AuthParticipant(ModelParticipantAuthData authData)
        {
            var result = await PostToApi<ModelParticipantAuthData, TokenModel>("/api/auth/participant", authData);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                AddJwtToken(result.Result.Token);
            }

            return result;
        }
    }
}
using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;

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
        /// <param name="navigationManager">Navigation.</param>
        public AuthService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
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
                _authStateService.AddJwtToken(result.Result.Token);
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
                _authStateService.AddJwtToken(result.Result.Token);
            }

            return result;
        }
    }
}
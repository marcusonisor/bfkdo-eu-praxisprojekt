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
        /// <param name="authStateService">AuthStateService.</param>
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

        /// <summary>
        /// Setzt die KontextId des Bewerters.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<int>> SetEvaluatorContextId(string password)
        {
            var result = await PostToApi<string, int>("/api/auth/evaluator/getcontextid", password);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                _authStateService.AddContextId(result.Result);
            }

            return result;
        }

        /// <summary>
        /// Setzt die KontextId des Teilnehmers.
        /// </summary>
        /// <param name="sybosId"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<int>> SetParticipantContextId(int sybosId)
        {
            var result = await PostToApi<int, int>("/api/auth/participant/getcontextid", sybosId);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                _authStateService.AddContextId(result.Result);
            }

            return result;
        }
    }
}
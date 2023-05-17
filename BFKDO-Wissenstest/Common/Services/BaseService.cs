using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Common.Services
{
    /// <summary>
    ///     Base Service für die Kommunikation vom Client zum Server.
    /// </summary>
    public class BaseService
    {
        private readonly HttpClient _httpClient;

        private readonly NavigationManager _navigationManager;

        /// <summary>
        ///     Authentication State Service.
        /// </summary>
        protected readonly AuthenticationStateService _authStateService;

        /// <summary>
        ///     Konstruktor des Base Services.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="authStateService">Authentication State Service.</param>
        /// <param name="navigationManager">Navigation.</param>
        public BaseService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager)
        {
            _httpClient = client;
            _authStateService = authStateService;
            _navigationManager = navigationManager;
        }

        /// <summary>
        ///     Hinzufügen der Authentifizierung zum Call.
        /// </summary>
        /// <returns></returns>
        private void AddAuthentication()
        {
            var token = _authStateService.GetJwtToken();

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        ///     Get Methode mit Fehlerbehandlung.
        /// </summary>
        /// <typeparam name="T">Typparameter.</typeparam>
        /// <param name="url">Url für Abruf.</param>
        /// <returns>Request Result.</returns>
        protected async Task<HttpRequestResult<T>> GetFromApi<T>(string url)
        {
            AddAuthentication();

            try
            {
                var response = await _httpClient.GetAsync(url);

                var result = await HandleHttpResponse<T>(response);

                CheckAuthenticationError(result);

                return result;
            }
            catch (Exception)
            {
                return GetFailedAPICallResult<T>();
            }
        }

        /// <summary>
        ///     Post Methode mit Fehlerbehandlung.
        /// </summary>
        /// <typeparam name="T">Typ des Übergabeparameters.</typeparam>
        /// <typeparam name="U">Typ des Ergebnisses.</typeparam>
        /// <param name="url">Url für Aufruf.</param>
        /// <param name="content">Übergabeparemeter.</param>
        /// <returns>Request Result.</returns>
        protected async Task<HttpRequestResult<U>> PostToApi<T, U>(string url, T content)
        {
            AddAuthentication();

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, content);

                var result = await HandleHttpResponse<U>(response);
                
                CheckAuthenticationError(result);

                return result;
            }
            catch (Exception)
            {
                return GetFailedAPICallResult<U>();
            }

        }

        /// <summary>
        ///     Put Methode mit Fehlerbehandlung.
        /// </summary>
        /// <typeparam name="T">Typ des Übergabeparameters.</typeparam>
        /// <typeparam name="U">Typ des Ergebnisses.</typeparam>
        /// <param name="url">Url für Aufruf.</param>
        /// <param name="content">Übergabeparemeter.</param>
        /// <returns>Request Result.</returns>
        protected async Task<HttpRequestResult<U>> PutToApi<T, U>(string url, T content)
        {
            AddAuthentication();

            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, content);

                var result = await HandleHttpResponse<U>(response);

                CheckAuthenticationError(result);

                return result;
            }
            catch (Exception)
            {
                return GetFailedAPICallResult<U>();
            }
        }

        /// <summary>
        ///     Delete Methode mit Fehlerbehandlung.
        /// </summary>
        /// <typeparam name="T">Typparameter.</typeparam>
        /// <param name="url">Url für Abruf.</param>
        /// <returns>Request Result.</returns>
        protected async Task<HttpRequestResult<T>> DeleteFromApi<T>(string url)
        {
            AddAuthentication();

            try
            {
                var response = await _httpClient.DeleteAsync(url);

                var result = await HandleHttpResponse<T>(response);

                CheckAuthenticationError(result);

                return result;
            }
            catch (Exception)
            {
                return GetFailedAPICallResult<T>();
            }
        }

        /// <summary>
        ///     Überprüfen eines Authentication Errors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckAuthenticationError<T>(HttpRequestResult<T> result)
        {
            if (result.RequestEnum is EnumHttpRequest.Forbidden or EnumHttpRequest.Unauthorized)
            {
                _authStateService.DeleteJwtToken();
                _navigationManager.NavigateTo("/");
            }
        }

        /// <summary>
        ///     Verarbeitung des HTTP Responses.
        /// </summary>
        /// <typeparam name="T">Typparameter der Rückgabe.</typeparam>
        /// <param name="response">Response von Server.</param>
        /// <returns>Verarbeiteter Request.</returns>
        private async static Task<HttpRequestResult<T>> HandleHttpResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
            {
                return new HttpRequestResult<T>
                {
                    RequestEnum = EnumHttpRequest.UnknownError
                };
            }

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new HttpRequestResult<T>
                    {
                        RequestEnum = EnumHttpRequest.Unauthorized,
                        ErrorMessage = "Nicht authoriziert für diesen Call!"
                    };
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return new HttpRequestResult<T>
                    {
                        RequestEnum = EnumHttpRequest.Forbidden,
                        ErrorMessage = "Authentifiziert, aber mit falscher Rolle"!
                    };
                }

                return new HttpRequestResult<T>
                {
                    RequestEnum = EnumHttpRequest.UnknownError,
                    ErrorMessage = $"Server hat mit {response.StatusCode} geantwortet!"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
            {
                return new HttpRequestResult<T>
                {
                    RequestEnum = EnumHttpRequest.UnknownError,
                    ErrorMessage = "Fehler beim Lesen/Parsen des Ergebnisses."
                };
            }

            return new HttpRequestResult<T>
            {
                RequestEnum = EnumHttpRequest.Success,
                Result = result
            };
        }

        private static HttpRequestResult<T> GetFailedAPICallResult<T>()
        {
            return new HttpRequestResult<T>
            {
                RequestEnum = EnumHttpRequest.UnknownError,
                ErrorMessage = "Daten konnte nicht abgerufen werden!"
            };
        }
    }
}

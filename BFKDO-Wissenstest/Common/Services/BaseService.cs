using System.Net.Http.Json;

namespace Common.Services
{
    /// <summary>
    ///     Base Service für die Kommunikation vom Client zum Server.
    /// </summary>
    public class BaseService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Konstruktor des Base Services.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        public BaseService(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        ///     Hinzufügen der Authentifizierung zum Call.
        /// </summary>
        /// <returns></returns>
        private void AddAuthentication()
        {
            //localstorage.getItem(jwt)
            //http.addrequestheader("authentication",jwt);
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

            var response = await _httpClient.GetAsync(url);

            var result = await HandleHttpResponse<T>(response);

            return result;
        }

        /// <summary>
        ///     Post Methode mit Fehlerbehandlung.
        /// </summary>
        /// <typeparam name="T">Typ des Übergabeparameters.</typeparam>
        /// <typeparam name="U">Typ des Ergebnisses.</typeparam>
        /// <param name="url">Url für Aufruf.</param>
        /// <param name="content">Übergabeparemeter.</param>
        /// <returns>Request Result.</returns>
        protected async Task<HttpRequestResult<U>> PostToApi<T,U>(string url, T content)
        {
            AddAuthentication();

            var response = await _httpClient.PostAsJsonAsync(url,content);

            var result = await HandleHttpResponse<U>(response);

            return result;
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

            var response = await _httpClient.PutAsJsonAsync(url, content);

            var result = await HandleHttpResponse<U>(response);

            return result;
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

            var response = await _httpClient.DeleteAsync(url);

            var result = await HandleHttpResponse<T>(response);

            return result;
        }

        /// <summary>
        ///     Verarbeitung des HTTP Responses.
        /// </summary>
        /// <typeparam name="T">Typparameter der Rückgabe.</typeparam>
        /// <param name="response">Response von Server.</param>
        /// <returns>Verarbeiteter Request.</returns>
        private async Task<HttpRequestResult<T>> HandleHttpResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
            {
                return new HttpRequestResult<T>
                {
                    WasSuccess = false
                };
            }

            if (!response.IsSuccessStatusCode)
            {
                return new HttpRequestResult<T>
                {
                    WasSuccess = false,
                    ErrorMessage = $"Server hat mit {response.StatusCode} geantwortet!"
                };
            }

            var result = await response.Content.ReadFromJsonAsync<T>();
            if (result == null)
            {
                return new HttpRequestResult<T>
                {
                    WasSuccess = false,
                    ErrorMessage = ""
                };
            }

            return new HttpRequestResult<T>
            {
                WasSuccess = true,
                Result = result
            };
        }
    }

    /// <summary>
    ///     HTTP Request Result.
    /// </summary>
    /// <typeparam name="T">Typ des Rückgabetypens</typeparam>
    public class HttpRequestResult<T>
    {
        /// <summary>
        ///     Boolean, ob der Request erfolgreich war.
        /// </summary>
        public bool WasSuccess { get; set; }

        /// <summary>
        ///     Fehlernachricht bei aufgetretenen Fehler.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        ///     Ergebnis des Requests.
        /// </summary>
        public T Result { get; set; } = default(T)!;
    }
}

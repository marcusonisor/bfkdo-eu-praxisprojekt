using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Common.Services
{
    /// <summary>
    ///     Authentication Status Service.
    /// </summary>
    public class AuthenticationStateService
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        private readonly NavigationManager _navigationManager;

        /// <summary>
        ///     Konstruktor des Authentication State Services.
        /// </summary>
        /// <param name="storageService">Local Storage Service.</param>
        /// <param name="navigationManager">Zur Navigation</param>
        public AuthenticationStateService(ISyncLocalStorageService storageService, NavigationManager navigationManager)
        {
            _syncLocalStorageService = storageService;
            _navigationManager = navigationManager;
        }

        /// <summary>
        ///     Weiterleitung bei fehlender Authentifizierung.
        /// </summary>
        public void CheckJwtAuthentication()
        {
            if (!IsLoggedIn())
            {
                _navigationManager.NavigateTo("/");
            }
        }

        /// <summary>
        ///     Hinzufügen des Tokens.
        /// </summary>
        /// <param name="token">Jwt Token.</param>
        public void AddJwtToken(string token)
        {
            _syncLocalStorageService.SetItem("jwt", token);
        }

        /// <summary>
        ///     Abrufen des Tokens.
        /// </summary>
        public string GetJwtToken()
        {
            return _syncLocalStorageService.GetItem<string>("jwt");
        }

        /// <summary>
        ///     Entfernen des Tokens.
        /// </summary>
        public void DeleteJwtToken()
        {
            _syncLocalStorageService.RemoveItem("jwt");
        }

        /// <summary>
        ///     Hinzufügen der ContextId.
        /// </summary>
        /// <param name="contextId">Context ID.</param>
        public void AddContextId(int contextId)
        {
            _syncLocalStorageService.SetItem("contextId", contextId);
        }

        /// <summary>
        ///     Abrufen der ContextId.
        /// </summary>
        public int GetContextId()
        {
            return _syncLocalStorageService.GetItem<int>("contextId");
        }

        /// <summary>
        ///     Entfernen der ContextId.
        /// </summary>
        public void DeleteContextId()
        {
            _syncLocalStorageService.RemoveItem("contextId");
        }

        /// <summary>
        ///     Checkt, ob der Benutzer eingeloggt ist.
        /// </summary>
        /// <returns>Ob ein gültiger JWT Token hinterlegt ist.</returns>
        public bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(_syncLocalStorageService.GetItem<string>("jwt"));
        }
    }
}

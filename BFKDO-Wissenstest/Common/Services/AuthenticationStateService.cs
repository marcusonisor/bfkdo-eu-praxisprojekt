using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        ///     Checkt, ob eine Authentifizierung vorhanden ist.
        /// </summary>
        /// <returns>Ob Authentifizierungs Token vorhanden ist.</returns>
        public void CheckJwtAuthentication()
        {
            if (string.IsNullOrWhiteSpace(_syncLocalStorageService.GetItem<string>("jwt")))
            {
                _navigationManager.NavigateTo("/");
            }
        }
    }
}

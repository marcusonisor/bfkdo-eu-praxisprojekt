using Blazored.LocalStorage;
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

        /// <summary>
        ///     Konstruktor des Authentication State Services.
        /// </summary>
        /// <param name="storageService">Local Storage Service.</param>
        public AuthenticationStateService(ISyncLocalStorageService storageService)
        {
            _syncLocalStorageService = storageService;
        }

        /// <summary>
        ///     Checkt, ob eine Authentifizierung vorhanden ist.
        /// </summary>
        /// <returns>Ob Authentifizierungs Token vorhanden ist.</returns>
        public bool HasJwtAuthentication()
        {
            return !string.IsNullOrWhiteSpace(_syncLocalStorageService.GetItem<string>("jwt"));
        }
    }
}

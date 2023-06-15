using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Models;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Services
{
    /// <summary>
    /// Der Teilnehmerservice.
    /// </summary>
    public class ParticipantService : BaseService
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authStateService"></param>
        /// <param name="navigationManager"></param>
        public ParticipantService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
        {
        }

        /// <summary>
        /// Ruft die Dashboarddaten ab.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Die Dashboarddaten.</returns>
        public async Task<HttpRequestResult<List<ModelExaminationLevel>>> GetDashboardData(int id)
        {
            var result = await GetFromApi<List<ModelExaminationLevel>>($"api/participant/{id}/getdashboarddata");

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }
    }
}
using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Models;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Services
{
    public class ParticipantService : BaseService
    {

        public ParticipantService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
        {
        }

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
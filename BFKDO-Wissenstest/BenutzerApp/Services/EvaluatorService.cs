using BenutzerApp.Pages.Evaluator;
using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Services
{
    /// <summary>
    /// Service für den Testbewerter.
    /// </summary>
    public class EvaluatorService : BaseService
    {
        /// <summary>
        /// Konstruktor des StationsService.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="authStateService">Authentication Status Service.</param>
        /// <param name="navigationManager">Navigation Manager.</param>
        public EvaluatorService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
        {
        }

        /// <summary>
        /// Retourniert eine Liste mit allen Stationen.
        /// </summary>
        /// <returns>Liste mit allen Stationen.</returns>
        public async Task<HttpRequestResult<List<TestStationModel>>> GetAllTestStations(int knowledgeTestId)
        {
            var result = await GetFromApi<List<TestStationModel>>($"/api/evaluator/getstations/{knowledgeTestId}");

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }

        /// <summary>
        /// Ruft die Dashboarddaten ab.
        /// </summary>
        /// <param name="knowledgeTestId"></param>
        /// <param name="stationId"></param>
        /// <returns>Die Dashboarddaten.</returns>
        public async Task<HttpRequestResult<List<ModelEvaluationSet>>> GetStationData(int knowledgeTestId, int stationId)
        {
            var result = await GetFromApi<List<ModelEvaluationSet>>($"/api/evaluator/getstationdata/{stationId}/{knowledgeTestId}");

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }

        /// <summary>
        /// Rufen den Namen der angegebenen Station ab.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>Der Name der angegebenen Stationen.</returns>
        public async Task<HttpRequestResult<ModelStationName>> GetStationName(int stationId)
        {
            var result = await GetFromApi<ModelStationName>($"/api/evaluator/getstationname/{stationId}");

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }

        /// <summary>
        /// Benotet die ausgewählte Evaluierung.
        /// </summary>
        /// <param name="evaluation"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<bool>> SubmitEvaluation(ModelEvaluation evaluation)
        {
            var result = await PostToApi<ModelEvaluation, bool>($"/api/evaluator/submitevaluation", evaluation);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }

        /// <summary>
        /// Schließt die ausgewählten Evaluierungen ab.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<bool>> CloseEvaluation(List<int> ids)
        {
            var result = await PostToApi<List<int>, bool>($"/api/evaluator/closeevaluation", ids);

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }

            return result;
        }
    }
}
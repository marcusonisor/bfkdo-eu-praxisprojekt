using Blazored.LocalStorage;
using Common.Enums;
using Common.Model;
using Common.Services;

namespace BenutzerApp.Services
{
    /// <summary>
    /// Service für Stationen.
    /// </summary>
    public class StationService : BaseService
    {
        /// <summary>
        /// Konstruktor des StationsService.
        /// </summary>
        /// <param name="client">HTTP Client.</param>
        /// <param name="storageService">SpeicherService.</param>
        public StationService(HttpClient client, ISyncLocalStorageService storageService) : base(client,storageService)
        {
        }

        /// <summary>
        /// Retourniert eine Liste mit allen Stationen.
        /// </summary>
        /// <returns>Liste mit allen Stationen.</returns>
        public async Task<HttpRequestResult<List<TestStationModel>>> GetAllTestStations()
        {
            var result = await GetFromApi<List<TestStationModel>>("/api/evaluator/getstations");

            if (result.RequestEnum == EnumHttpRequest.Success)
            {
                Console.WriteLine(result.Result);
            }
            return result;
        }
    }
}
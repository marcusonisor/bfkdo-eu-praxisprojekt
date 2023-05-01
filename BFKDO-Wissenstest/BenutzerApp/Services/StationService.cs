namespace BenutzerApp.Services
{
    using Common.Model;
    using Common.Services;

    public class StationService : BaseService
    {
        public StationService(HttpClient client) : base(client)
        {
        }

        public async Task<HttpRequestResult<List<TestStationModel>>> GetAllTestStations()
        {
            var result = await GetFromApi<List<TestStationModel>>("/api/evaluator/getstations");

            if (result.WasSuccess)
            {
                Console.WriteLine(result.Result);
            }
            return result;
        }

        public async Task<HttpRequestResult<string>> Getkek()
        {
            var result = await GetFromApi<string>("/api/evaluator/kek");

           
            return result;
        }
    }
}
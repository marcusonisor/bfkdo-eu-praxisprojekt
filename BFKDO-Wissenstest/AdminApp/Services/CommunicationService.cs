using Common.Model.CSVModels;
using Common.Services;

namespace AdminApp.Services
{
    /// <summary>
    ///     Service zur Kommunikation mit dem Server.
    /// </summary>
    public class CommunicationService : BaseService
    {
        /// <summary>
        ///     Konstruktor des Kommunikationservices.
        /// </summary>
        /// <param name="client"></param>
        public CommunicationService(HttpClient client) : base(client)
        {
        }

        /// <summary>
        ///     Registrierungen 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<bool>> PostRegistrationsFromFile(byte[] data)
        {
            var result = await PostToApi<byte[], bool>("/api/registration/readfromcsv", data);
            return result;
        }
    }
}

using Common.Services;

namespace BenutzerApp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CommunicationService : BaseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public CommunicationService(HttpClient client) : base(client)
        {
        }
    }
}

using Blazored.LocalStorage;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AdminApp.Services
{
    /// <summary>
    /// Service für den Export von Zugangsdaten.
    /// </summary>
    public class ExportService : BaseService
    {
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// Konstruktor des Services.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="storageService"></param>
        /// <param name="navigationManager"></param>
        /// <param name="jsRuntime"></param>
        public ExportService(HttpClient client, ISyncLocalStorageService storageService, NavigationManager navigationManager, IJSRuntime jsRuntime) : base(client, storageService, navigationManager)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Lädt die Zugangsdaten des Testbewerters per JSInterop runter.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten des Testbewerters</returns>
        public async Task DownloadEvaluatorCredentials(int knowledgetestId)
        {
            var response = await GetEvaluatorCredentials(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", "file.docx", response.Result);
        }

        /// <summary>
        /// Lädt die Zugangsdaten der Testteilnehmer eines spezifischen Tests per JSInterop runter.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten der Testteilnehmer eines spezifischen Tests</returns>
        public async Task DownloadParticipantsCredentials(int knowledgetestId)
        {
            var response = await GetParticipantsCredentials(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", "file.docx", response.Result);
        }

        /// <summary>
        /// Holt die Zugangsdaten als byte[] vom Server.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten als byte[]</returns>
        private async Task<HttpRequestResult<byte[]>> GetEvaluatorCredentials(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"/api/admin/export/getevaluatorcredentials/{knowledgetestId}");
        }

        /// <summary>
        /// Holt die Zugangsdaten als byte[] vom Server.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten als byte[]</returns>
        private async Task<HttpRequestResult<byte[]>> GetParticipantsCredentials(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"/api/admin/export/getparticipantscredentials/{knowledgetestId}");
        }
    }
}
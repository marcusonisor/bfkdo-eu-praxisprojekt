using Blazored.LocalStorage;
using Common.Model;
using Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AdminApp.Services
{
    public class ExportService : BaseService
    {
        private readonly IJSRuntime _jsRuntime;

        public ExportService(HttpClient client, ISyncLocalStorageService storageService, NavigationManager navigationManager, IJSRuntime jsRuntime) : base(client, storageService, navigationManager)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ExportEvaluatorCredentials(int knowledgetestId)
        {
            var response = await GetEvaluatorCredentials(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", "file.docx", response.Result);
        }

        /// <summary>
        ///     Details einer Wissenstestung abrufen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<HttpRequestResult<byte[]>> GetEvaluatorCredentials(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"/api/admin/export/getevaluatorcredentials/{knowledgetestId}");
        }
    }
}
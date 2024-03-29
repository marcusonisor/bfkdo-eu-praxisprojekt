﻿using Blazored.LocalStorage;
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

        private readonly string _resultExportFilePrefix = "ergebnisse_";
        private readonly string _evaluatorExportFilePrefix = "bewerter_";
        private readonly string _participantExportFilePrefix = "teilnehmer_";
        private readonly string _exportFileSuffix = ".docx";
        private readonly string _exportCSVFileSuffix = ".csv";

        /// <summary>
        /// Konstruktor des Services.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="authStateService"></param>
        /// <param name="navigationManager"></param>
        /// <param name="jsRuntime"></param>
        public ExportService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager, IJSRuntime jsRuntime) : base(client, authStateService, navigationManager)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Lädt die Zugangsdaten des Testbewerters per JSInterop runter.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <param name="fileName"></param>
        /// <returns>Die Zugangsdaten des Testbewerters</returns>
        public async Task DownloadEvaluatorCredentials(int knowledgetestId, string fileName)
        {
            var response = await GetEvaluatorCredentials(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", _evaluatorExportFilePrefix + fileName + _exportFileSuffix, response.Result);
        }

        /// <summary>
        /// Lädt die Zugangsdaten der Testteilnehmer eines spezifischen Tests per JSInterop runter.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <param name="fileName"></param>
        /// <returns>Die Zugangsdaten der Testteilnehmer eines spezifischen Tests</returns>
        public async Task DownloadParticipantsCredentials(int knowledgetestId, string fileName)
        {
            var response = await GetParticipantsCredentials(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", _participantExportFilePrefix + fileName + _exportFileSuffix, response.Result);
        }

        /// <summary>
        ///     Exportieren der Ergebnisse mit einer CSV.
        /// </summary>
        /// <param name="knowledgetestId">Wissenstest Id.</param>
        /// <param name="fileName">Dateiname.</param>
        /// <returns>Ergebnisse als CSV.</returns>
        public async Task DownloadResultsAsCsv(int knowledgetestId, string fileName)
        {
            var response = await GetResultsAsCSV(knowledgetestId);

            await _jsRuntime.InvokeVoidAsync("downloadFile", _resultExportFilePrefix + fileName + _exportCSVFileSuffix, response.Result);
        }

        /// <summary>
        /// Holt die Zugangsdaten als byte[] vom Server.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten als byte[]</returns>
        private async Task<HttpRequestResult<byte[]>> GetEvaluatorCredentials(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"/api/export/evaluatorcredentials/{knowledgetestId}");
        }

        /// <summary>
        /// Holt die Zugangsdaten als byte[] vom Server.
        /// </summary>
        /// <param name="knowledgetestId"></param>
        /// <returns>Die Zugangsdaten als byte[]</returns>
        private async Task<HttpRequestResult<byte[]>> GetParticipantsCredentials(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"/api/export/participantscredentials/{knowledgetestId}");
        }

        /// <summary>
        ///     Ergebnisse als CSV exportieren.
        /// </summary>
        /// <param name="knowledgetestId">Wissenstest Id.</param>
        /// <returns>CSV Bytes.</returns>
        private async Task<HttpRequestResult<byte[]>> GetResultsAsCSV(int knowledgetestId)
        {
            return await GetFromApi<byte[]>($"api/export/GetResultCsv/{knowledgetestId}");
        }
    }
}
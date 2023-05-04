﻿using AdminApp.Pages;
using Blazored.LocalStorage;
using Common.Model;
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
        /// <param name="client">HTTP Client.</param>
        /// <param name="storageService">Speicher Service.</param>
        public CommunicationService(HttpClient client, ISyncLocalStorageService storageService) : base(client, storageService)
        {
        }

        /// <summary>
        ///     Registrierungen durch Tabelle anlegen.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<bool>> PostRegistrationsFromFile(byte[] data)
        {
            var result = await PostToApi<byte[], bool>("/api/Registration/ReadRegistrationsFromCsv", data);
            return result;
        }

        /// <summary>
        ///     Details einer Wissenstestung abrufen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpRequestResult<ModelKnowledgeTestDetails>> GetKnowledgeTestDetails(int id)
        {
            var result = await GetFromApi<ModelKnowledgeTestDetails>($"/api/knowledgetest/getknowledgetestdetails/{id}");
            return result;
        }
    }
}

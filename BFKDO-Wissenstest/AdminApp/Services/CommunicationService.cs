using AdminApp.Pages;
using Blazored.LocalStorage;
using Common.Model;
using Common.Model.CSVModels;
using Common.Services;
using Microsoft.AspNetCore.Components;

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
        /// <param name="authStateService">Authentication State Service.</param>
        /// <param name="navigationManager">Navigation.</param>
        public CommunicationService(HttpClient client, AuthenticationStateService authStateService, NavigationManager navigationManager) : base(client, authStateService, navigationManager)
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
        ///     Holt die Liste an Wissenstests.
        /// </summary>
        /// <returns>Liste an Wissenstests.</returns>
        public async Task<HttpRequestResult<List<ModelKnowledgeTest>>> GetKnowledgeTests()
        {
            var result = await GetFromApi<List<ModelKnowledgeTest>>("api/knowledgetest/GetKnowledgeTests");
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

        /// <summary>
        ///     Erstellen eines Wissenstest.
        /// </summary>
        /// <param name="designation">Beschreibung des Wissenstest.</param>
        /// <returns>Id des angelegten Wissenstest.</returns>
        public async Task<HttpRequestResult<int>> CreateKnowledgeTest(string designation)
        {
            var result = await PostToApi<string, int>("api/knowledgetest/CreateKnowledgeTest", designation);
            return result;
        }

        /// <summary>
        ///     Importieren der Testpersonen über die CSV.
        /// </summary>
        /// <param name="data">Daten.</param>
        /// <returns>Ob Testpersonen importiert wurden.</returns>
        public async Task<HttpRequestResult<bool>> ImportRegistrations(ModelImportData data)
        {
            var result = await PostToApi<ModelImportData, bool>("api/knowledgetest/ImportRegistrations", data);
            return result;
        } 

        /// <summary>
        ///     Löschen des Wissenstest.
        /// </summary>
        /// <param name="id">Id des Wissenstest.</param>
        /// <returns>Ob der Test gelöscht wurde.</returns>
        public async Task<HttpRequestResult<bool>> DeleteKnowledgeTest(int id)
        {
            var result = await DeleteFromApi<bool>($"api/knowledgetest/DeleteKnowledgeTest/{id}");
            return result;
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using AdminApp.Services;
using Common.Model;
using Common.Enums;
using MudBlazor;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Importieren der Testpersonen.
    /// </summary>
    public partial class ImportTestpersons
    {
        private readonly long maxFileSize = 1024 * 1024 * 15;

        /// <summary>
        ///     Wissentest Id.
        /// </summary>
        [Parameter]
        public int KnowledgeTestId { get; set; }

        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Navigationsmanager für Weiterleitungen.
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        /// <summary>
        ///     Daten geladen oder nicht.
        /// </summary>
        public bool DataLoaded { get; set; }

        /// <summary>
        ///     Ist Overlay sichtbar oder nicht.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Methode für den Upload von Files.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task UploadFile(IBrowserFile file)
        {
            if (file != null)
            {
                IsVisible = true;
                var buffer = new byte[file.Size];
                _ = await file.OpenReadStream(maxFileSize).ReadAsync(buffer);
                var model = new ModelImportData() { CsvData=buffer, KnowledgeTestId=KnowledgeTestId};
                var response = await Service.ImportRegistrations(model);
                IsVisible = false;
                DataLoaded = true;

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    NavigationManager.NavigateTo($"/knowledgetestdetails/{KnowledgeTestId}");
                }
                else
                {
                    MudSnackbar.Add("Import fehlgeschlagen! Bitte versuchen Sie es noch einmal.", Severity.Error);
                }
            }
        }
    }
}
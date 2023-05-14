using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using AdminApp.Services;
using Common.Model;
using Common.Enums;
using MudBlazor;

namespace AdminApp.Pages
{
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

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        /// <summary>
        /// Methode für den Upload von Files.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task UploadFile(IBrowserFile file)
        {
            if (file != null)
            {
                var buffer = new byte[file.Size];
                _ = await file.OpenReadStream(maxFileSize).ReadAsync(buffer);
                var model = new ModelImportData() { CsvData=buffer, KnowledgeTestId=KnowledgeTestId};
                var response = await Service.ImportRegistrations(model);

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
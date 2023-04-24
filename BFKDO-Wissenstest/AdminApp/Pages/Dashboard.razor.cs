using AdminApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AdminApp.Pages
{
    public partial class Dashboard
    {
        private long maxFileSize = 1024 * 1024 * 15;

        /// <summary>
        ///     Navigation Manager.
        /// </summary>
        [Inject]
        public NavigationManager Nav { get; set; } = null!;

        /// <summary>
        ///     Test-String fürs Bytes auslesen.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        private void NavigateToDetails()
        {
            if (Nav != null)
            {
                Nav.NavigateTo("/knowledgetestdetails");
            }
        }

        private async Task UploadFile(IBrowserFile file)
        {
            var bytestream = file.OpenReadStream();
            if (file != null)
            {
                var buffer = new byte[file.Size];
                var length = await file.OpenReadStream(maxFileSize).ReadAsync(buffer);

                Message = $"{buffer.Length} Bytes aus dem File gelesen!";
            }
        }
    }
}
using AdminApp.Services;
using Microsoft.AspNetCore.Components;

namespace AdminApp.Pages
{
    /// <summary>
    /// Dashboard
    /// </summary>
    public partial class Dashboard
    {
        /// <summary>
        /// Navigation Manager.
        /// </summary>
        [Inject]
        public NavigationManager Nav { get; set; } = null!;

        /// <summary>
        /// Kommunikations Service.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            // TODO Abfrage der Wissenstests.
        }

        private void NavigateToDetails()
        {
            //  TODO ausgewählte ID statt 4.
            Nav?.NavigateTo("/knowledgetestdetails/4");
        }
    }
}
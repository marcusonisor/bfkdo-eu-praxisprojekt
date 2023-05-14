using Microsoft.AspNetCore.Components;
using AdminApp.Services;
using Common.Enums;
using MudBlazor;

namespace AdminApp.Pages
{
    public partial class CreateKnowledgeTest
    {
        /// <summary>
        ///     Kommunikationsservice.
        /// </summary>
        [Inject]
        public CommunicationService Service { get; set; } = null!;

        public string Year { get; set; }
        
        async void SaveYear()
        {
            var response = await Service.CreateKnowledgeTest(Year);

            if (response.RequestEnum is EnumHttpRequest.Success)
            {
                // Redirect to the main application or perform other actions
                NavigationManager.NavigateTo($"/importtestpersons/{response.Result}");
            }

            else
            {
                // Show error message
                MudSnackbar.Add("Invalid email or password. Please try again.", Severity.Error);
            }
        }


    }
}
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

        /// <summary>
        ///     Variable zum Speichern des Testjahres.
        /// </summary>
        public string Year { get; set; } = String.Empty;

        /// <summary>
        ///     Methode zum Anlegen des Wissenstests. 
        /// </summary>
        async void CreateTest()
        {
            var response = await Service.CreateKnowledgeTest(Year);

            if (response.RequestEnum is EnumHttpRequest.Success)
            {            
                NavigationManager.NavigateTo($"/importtestpersons/{response.Result}");
            }

            else
            {
                MudSnackbar.Add("Ungültiger Benutzername oder Passwort. Bitte versuchen Sie es erneut.", Severity.Error);
            }
        }
    }
}
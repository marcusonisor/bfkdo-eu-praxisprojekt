using Microsoft.AspNetCore.Components;
using AdminApp.Services;
using Common.Enums;
using MudBlazor;

namespace AdminApp.Pages
{
    /// <summary>
    ///     Erstellen des Wissenstest.
    /// </summary>
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
            if (!string.IsNullOrEmpty(Year))
            {
                var response = await Service.CreateKnowledgeTest(Year);

                if (response.RequestEnum is EnumHttpRequest.Success)
                {
                    NavigationManager.NavigateTo($"/importtestpersons/{response.Result}");
                }

                else if (response.RequestEnum is EnumHttpRequest.BadRequest)
                {
                    MudSnackbar.Add(response.ErrorMessage, Severity.Error);
                }
            }
            else
            {
                MudSnackbar.Add("Bitte geben Sie ein Testjahr ein!", Severity.Error);
            }
        }
    }
}
using BenutzerApp.Services;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Pages.Evaluator
{
    /// <summary>
    ///     Stationsänderung.
    /// </summary>
    public partial class Dashboard
    {
        private List<TestStationModel> _stations = new();

        //private TestStationModel _currentStation = new();

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var response = await StationService.GetAllTestStations();
            _stations = response.Result.OrderBy(e => e.CriteriaName).ToList();

            //_currentStation ??= _stations.FirstOrDefault()!;
        }

        /// <summary>
        ///     Stationen Service.
        /// </summary>
        [Inject]
        public EvaluatorService StationService { get; set; } = null!;

        /// <summary>
        /// Methode zum Ändern der Station
        /// </summary>
        public void SelectStation(int id)
        {
            NavigationManager.NavigateTo($"/evaluator/station/{id}");
        }
    }

}
using BenutzerApp.Services;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace BenutzerApp.Pages
{
    /// <summary>
    ///     Stationsänderung.
    /// </summary>
    public partial class StationChange
    {
        private List<TestStationModel> _stations = new();

        private TestStationModel _currentStation = new();

        /// <summary>
        ///     Initialisierungsmethode.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var response = await StationService.GetAllTestStations();
            _stations = response.Result;

            _currentStation ??= _stations.FirstOrDefault()!;
        }

        /// <summary>
        ///     Stationen Service.
        /// </summary>
        [Inject]
        public StationService StationService { get; set; } = null!;

        /// <summary>
        /// Methode zum Ändern der Station
        /// </summary>
        public void SelectStation(TestStationModel selectedStation)
        {
            _currentStation = selectedStation;
            // StateHasChanged();
        }
    }

}
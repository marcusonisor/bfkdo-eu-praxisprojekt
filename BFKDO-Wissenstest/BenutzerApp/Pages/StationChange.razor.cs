using Common.Model;

namespace BenutzerApp.Pages
{
    public partial class StationChange
    {
        /// <summary>
        /// Die Station zur Auswahl
        /// </summary>
        public List<TestStations> TestStations { get; set; } = new List<TestStations>()
        {
            new TestStations()
            {
                 CriteriaId = 1,
                 StationName = "adjalkdfjalkdf"
            },
            new TestStations()
            {
                 CriteriaId = 2,
                 StationName = "!)§($!)§P$()§P"
            },
            new TestStations()
            {
                 CriteriaId = 3,
                 StationName = "-.-.,-.,-p"
            },
            new TestStations()
            {
                 CriteriaId = 4,
                 StationName = "45878988"
            },
        };

        /// <summary>
        /// Die aktuell zugewiesene Station
        /// </summary>
        public TestStations CurrentStation { get; set; } = new TestStations();

        /// <summary>
        /// Methode zum Ändern der Station
        /// </summary>
        public void SelectStation(TestStations station)
        {

            CurrentStation = station;
            StateHasChanged();
        }
    }

}
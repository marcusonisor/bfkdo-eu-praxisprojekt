namespace Common.Model
{
    /// <summary>
    ///     Wechsel der Station.
    /// </summary>
    public class ModelStationChange
    {

        /// <summary>
        ///     Verfügbare Stationen.
        /// </summary>
        public List<TestStations> Stations { get; set; } = new(); 
    }

    /// <summary>
    ///     -
    /// </summary>
    public class TestStations
    {

        /// <summary>
        ///     ID der Stufe.
        /// </summary>
        public int CriteriaId { get; set; }

        /// <summary>
        ///     Name der Stufen der Stationen.
        /// </summary>
        public string StationName { get; set; } = string.Empty;
    
    }
}

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
        public List<TestStationModel> Stations { get; set; } = new(); 
    }
}
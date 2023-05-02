namespace Common.Model
{
    /// <summary>
    /// Entität einer Teststation.
    /// </summary>
    public class TestStationModel
    {

        /// <summary>
        /// ID der Stufe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name der Stufen der Stationen.
        /// </summary>
        public string CriteriaName { get; set; } = string.Empty;
    }
}
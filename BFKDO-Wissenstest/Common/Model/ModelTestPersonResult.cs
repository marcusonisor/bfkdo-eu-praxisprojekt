namespace Common.Model
{
    /// <summary>
    /// Ergebnis eines Wissenstest einer Testperson.
    /// </summary>
    public class ModelTestPersonResult
    {
        /// <summary>
        /// Name der Testperson
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Feuerwehrstation der Testperson.
        /// </summary>
        public string Station { get; set; } = string.Empty;

        /// <summary>
        /// Teilgenommene Stufen der Testperson.
        /// </summary>
        public List<ModelKnowledgeLevelResult> Results { get; set; } = new();
    }
}
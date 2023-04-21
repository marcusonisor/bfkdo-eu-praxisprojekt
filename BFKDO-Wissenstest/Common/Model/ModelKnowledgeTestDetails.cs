namespace Common.Model
{
    /// <summary>
    ///     Ergebnis eines Wissenstest einer Testperson.
    /// </summary>
    public class ModelKnowledgeTestDetails
    {
        /// <summary>
        ///     Name der Testperson
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Feuerwehrstation der Testperson.
        /// </summary>
        public string Station { get; set; } = string.Empty;

        /// <summary>
        ///     Teilgenommene Stufen der Testperson.
        /// </summary>
        public List<ModelKnowledgeLevelResult> Results { get; set; } = new(); 
    }

    /// <summary>
    ///     Testergebnis einer Wissenstestung.
    /// </summary>
    public class ModelKnowledgeLevelResult
    {
        /// <summary>
        ///     Name der Stufe.
        /// </summary>
        public string LevelName { get; set; } = string.Empty;

        /// <summary>
        ///     Ergebnis der Testung.
        /// </summary>
        public string LevelResult { get; set; } = string.Empty;
    }
}

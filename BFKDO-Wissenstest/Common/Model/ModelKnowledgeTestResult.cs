namespace Common.Model
{
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

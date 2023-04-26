namespace Common.Model
{
    /// <summary>
    ///     Details einer Wissenstestung.
    /// </summary>
    public class ModelKnowledgeTestDetails
    {
        /// <summary>
        ///     Id des Wissenstest.
        /// </summary>
        public int KnowledgeTestId { get; set; } 

        /// <summary>
        ///     Jahr eines Wissenstest.
        /// </summary>
        public string KnowledgeTestYear { get; set; } = string.Empty;

        /// <summary>
        ///     Ergebnisse der Testpersonen eines Wissenstest.
        /// </summary>
        public List<ModelTestPersonResult> TestPersonResults { get; set; } = new List<ModelTestPersonResult>();
    }
}

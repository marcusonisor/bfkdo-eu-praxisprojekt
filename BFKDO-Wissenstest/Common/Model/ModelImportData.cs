namespace Common.Model
{
    /// <summary>
    ///     Teilnehmer Importierungsdaten.
    /// </summary>
    public class ModelImportData
    {
        /// <summary>
        ///     Id des Wissenstest.
        /// </summary>
        public int KnowledgeTestId { get; set; }    

        /// <summary>
        ///     Csv Wissenstest Data.
        /// </summary>
        public byte[] CsvData { get; set; } = new byte[0];
    }
}

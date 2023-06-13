namespace Common.Models
{
    /// <summary>
    /// DTO einer Wissensstufe.
    /// </summary>
    public class ModelExaminationLevel
    {
        /// <summary>
        /// Id der Wissensstufe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name der Wissensstufe.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Boolean für den Zustand der Wissensstufe.
        /// </summary>
        public bool Disabled { get; set; } = true;

        /// <summary>
        /// Ergebnisse der der Wissensstufe.
        /// </summary>
        public List<ModelExaminationResult> Results { get; set; } = new();
    }
}

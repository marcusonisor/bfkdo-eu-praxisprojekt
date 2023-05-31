namespace Common.Model
{
    /// <summary>
    /// Entität eines QRCodes.
    /// </summary>
    public class QRCodeModel
    {

        /// <summary>
        /// ID der Testperson.
        /// </summary>
        public int SybosId { get; set; }

        /// <summary>
        /// Bytes des QRCode
        /// </summary>
        public string QRCodeBytes { get; set; } = string.Empty;
    }
}
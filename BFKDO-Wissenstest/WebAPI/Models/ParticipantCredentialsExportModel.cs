namespace WebAPI.Models
{
    /// <summary>
    /// Model für den Export der Zugangsdaten des Testteilnehmers.
    /// </summary>
    public class ParticipantCredentialsExportModel
    {
        /// <summary>
        /// Konstruktor des Exportmodels.
        /// </summary>
        /// <param name="sybosId"></param>
        /// <param name="password"></param>
        public ParticipantCredentialsExportModel(string sybosId, string password, string qr)
        {
            SybosID = sybosId;
            Password = password;
            QR = qr;
        }

        /// <summary>
        /// SybosID des Testteilnehmers.
        /// </summary>
        public string SybosID { get; }

        /// <summary>
        /// Passwort des Testteilnehmers.
        /// </summary>
        public string Password { get; }

        public string QR { get; }
    }
}
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
        /// <param name="name"></param>
        /// <param name="sybosId"></param>
        /// <param name="password"></param>
        /// <param name="qr"></param>
        public ParticipantCredentialsExportModel(string name, string sybosId, string password, string qr)
        {
            Name = name;
            SybosID = sybosId;
            Password = password;
            QR = qr;
        }

        /// <summary>
        /// Voller Name des Testteilnehmers.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// SybosID des Testteilnehmers.
        /// </summary>
        public string SybosID { get; }

        /// <summary>
        /// Passwort des Testteilnehmers.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// QR-Code
        /// </summary>
        public string QR { get; }
    }
}
namespace WebAPI.Models
{
    /// <summary>
    /// Model für den Export der Zugangsdaten des Testbewerters.
    /// </summary>
    public class EvaluatorCredentialsExportModel
    {
        /// <summary>
        /// Konstruktor des Exportmodels.
        /// </summary>
        /// <param name="designation"></param>
        /// <param name="password"></param>
        public EvaluatorCredentialsExportModel(string designation, string password/*, Stream qr*/)
        {
            Designation = designation;
            Password = password;
            //QR = qr;
        }

        /// <summary>
        /// Bezeichnung des Tests.
        /// </summary>
        public string Designation { get; }

        /// <summary>
        /// Passwort des Testbewerters.
        /// </summary>
        public string Password { get; }

        //public Stream QR { get; }
    }
}
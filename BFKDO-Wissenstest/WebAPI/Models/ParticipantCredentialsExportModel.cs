namespace WebAPI.Models
{
    public class ParticipantCredentialsExportModel
    {
        public ParticipantCredentialsExportModel(string sybosId, string password, Stream qr)
        {
            SybosID = sybosId;
            Password = password;
            QR = qr;
        }

        public string SybosID { get; }

        public string Password { get; }

        public Stream QR { get; }
    }
}
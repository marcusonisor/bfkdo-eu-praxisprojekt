namespace WebAPI.Models
{
    public class EvaluatorCredentialsExportModel
    {
        public EvaluatorCredentialsExportModel(string designation, string password/*, Stream qr*/)
        {
            Designation = designation;
            Password = password;
            //QR = qr;
        }

        public string Designation { get; }

        public string Password { get; }

        //public Stream QR { get; }
    }
}
namespace Common.Model
{
    public class ModelParticipantAuthData
    {
        public ModelParticipantAuthData(string sybosId, string password)
        {
            SybosId = sybosId;
            Password = password;
        }

        public string SybosId { get; }

        public string Password { get; }
    }

    public class ModelEvaluatorAuthData
    {
        public ModelEvaluatorAuthData(string password)
        {
            Password = password;
        }

        public string Password { get; }
    }

    public class ModelAdminAuthData
    {
        public ModelAdminAuthData(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}

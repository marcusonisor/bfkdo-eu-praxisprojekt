namespace Common.Model
{
    /// <summary>
    /// DTO der Authentifizierungsdaten eines Admins.
    /// </summary>
    public class ModelAdminAuthData
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public ModelAdminAuthData(string email, string password)
        {
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Die E-Mail.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Das Password.
        /// </summary>
        public string Password { get; }
    }
}
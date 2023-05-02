namespace Common.Model
{
    /// <summary>
    /// DTO der Authentifizierungsdaten eines Testbewerters.
    /// </summary>
    public class ModelEvaluatorAuthData
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="password"></param>
        public ModelEvaluatorAuthData(string password)
        {
            Password = password;
        }

        /// <summary>
        /// Das Passwort.
        /// </summary>
        public string Password { get; }
    }
}
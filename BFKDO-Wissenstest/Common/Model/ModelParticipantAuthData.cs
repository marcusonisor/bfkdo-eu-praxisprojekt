namespace Common.Model
{
    /// <summary>
    /// DTO der Authentifizierungsdaten eines Testteilnehmers.
    /// </summary>
    public class ModelParticipantAuthData
    {
        /// <summary>
        /// Konstruktor des DTOs.
        /// </summary>
        /// <param name="sybosId"></param>
        /// <param name="password"></param>
        public ModelParticipantAuthData(int sybosId, string password)
        {
            SybosId = sybosId;
            Password = password;
        }

        /// <summary>
        /// Die Sybos ID.
        /// </summary>
        public int SybosId { get; }

        /// <summary>
        /// Das Passwort.
        /// </summary>
        public string Password { get; }
    }
}
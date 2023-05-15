namespace WebAPI.Models
{
    /// <summary>
    /// Model für den Export der Zugangsdaten mehrerer Testteilnehmer.
    /// </summary>
    public class ParticipantsCredentialsExportModel
    {
        /// <summary>
        /// Konstruktor des Exportmodels.
        /// </summary>
        /// <param name="participants"></param>
        public ParticipantsCredentialsExportModel(List<ParticipantCredentialsExportModel> participants)
        {
            Participants = participants;
        }

        /// <summary>
        /// Liste der Testteilnehmer.
        /// </summary>
        public List<ParticipantCredentialsExportModel> Participants { get; }
    }
}
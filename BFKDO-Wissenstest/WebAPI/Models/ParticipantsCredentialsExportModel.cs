namespace WebAPI.Models
{
    public class ParticipantsCredentialsExportModel
    {
        public ParticipantsCredentialsExportModel(List<ParticipantCredentialsExportModel> participants)
        {
            Participants = participants;
        }

        public List<ParticipantCredentialsExportModel> Participants { get; }
    }
}
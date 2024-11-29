using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class RegistrationDTO : Identifiable
    {
        public ParticipantDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }
}

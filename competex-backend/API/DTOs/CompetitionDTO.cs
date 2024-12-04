using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class CompetitionDTO : Identifiable
    {
        public required Guid CompetitionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Level Level { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public required int RegistrationPrice { get; set; }
    }
}

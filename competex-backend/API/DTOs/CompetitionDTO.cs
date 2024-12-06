using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class CompetitionDTO : Identifiable
    {
        public CompetitionTypeDTO? CompetitionType { get; set; }
        public Guid? CompetitionTypeId { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Level Level { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        public required int RegistrationPrice { get; set; }
        
    }
}

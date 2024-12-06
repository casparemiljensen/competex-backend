
namespace competex_backend.Models
{
    public class Competition : Identifiable
    {
        public required Guid CompetitionTypeId { get; set; }
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

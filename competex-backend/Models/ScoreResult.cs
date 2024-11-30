namespace competex_backend.Models
{
    public class ScoreResult : Identifiable
    {
        public int Faults { get; set; }
        public DateTime Time { get; set; }
        public Guid CompetitionId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}

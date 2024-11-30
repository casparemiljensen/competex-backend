namespace competex_backend.API.DTOs
{
    public class ScoreResultDTO : Identifiable
    {
        public int Faults { get; set; }
        public DateTime Time { get; set; }
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public ParticipantDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
    }
}

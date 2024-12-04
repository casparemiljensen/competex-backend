namespace competex_backend.API.DTOs
{
    public class ScoreResultDTO : Identifiable
    {
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public ParticipantDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
        public int Faults { get; set; }
        public TimeSpan Time { get; set; }
    }
}

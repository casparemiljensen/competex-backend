namespace competex_backend.API.DTOs
{
    public class ScoreResultDTO : Identifiable, IComparable<ScoreResultDTO>
    {
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public EkvipageDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
        public int Faults { get; set; }
        public TimeSpan Time { get; set; }

        public int CompareTo(ScoreResultDTO? other)
        {
            if (Faults == other.Faults)
            {
                return Time.CompareTo(other.Time);
            }
            return Faults - other.Faults;
        }
    }
}

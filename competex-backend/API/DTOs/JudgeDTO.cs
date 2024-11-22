namespace competex_backend.API.DTOs
{
    public class JudgeDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public List<MatchDTO>? Matches { get; set; } // Not sure if this should be MatchDTO or Match
    }
}

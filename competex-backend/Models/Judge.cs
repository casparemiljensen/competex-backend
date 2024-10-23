namespace competex_backend.Models
{
    public class Judge
    {
        public int JudgeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public int UserId { get; set; }
        public List<Match>? Matches { get; set; }
    }
}
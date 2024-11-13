namespace competex_backend.Models
{
    public class Judge : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public List<Match>? Matches { get; set; }
    }
}
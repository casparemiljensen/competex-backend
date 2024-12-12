
namespace competex_backend.Models
{
    public class CompetitionType : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public ScoreType ScoreType { get; set; }
        public ScoreMethod ScoreMethod { get; set; }
    }
}

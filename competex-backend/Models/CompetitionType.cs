
namespace competex_backend.Models
{
    public class CompetitionType : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        //public List<string> CompetitionAttributes { get; set; } = new List<string>();
        public ScoreType ScoreType { get; set; }
        public ScoreMethod ScoreMethod { get; set; }
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class CompetitionTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> CompetitionAttributes { get; set; } = new List<string>();
        public ScoreType ScoreType { get; set; }
        public ScoreMethod ScoreMethod { get; set; }
    }
}
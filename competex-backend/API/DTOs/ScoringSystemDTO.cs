using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class ScoringSystemDTO : Identifiable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ScoreType ScoreType { get; set; }
        public string ScoringRules { get; set; }
        public int Penalties { get; set; }

    }
}

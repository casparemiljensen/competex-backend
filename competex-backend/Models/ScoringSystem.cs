namespace competex_backend.Models
{
    public class ScoringSystem : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ScoreType ScoreType { get; set; }
        public string ScoringRules { get; set; } = string.Empty;
        public List<Guid> PenaltyIds { get; set; } = new List<Guid>();
        public Func<ScoreType, int, int> EvaluationMethod { get; set; }

    }
}

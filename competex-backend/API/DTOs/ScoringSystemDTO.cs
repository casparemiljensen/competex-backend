﻿using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class ScoringSystemDTO : Identifiable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ScoreType ScoreType { get; set; }
        public string ScoringRules { get; set; }
        public List<Penalty>? Penalties { get; set; }
        public List<Guid>? PenaltyIds { get; set; }
        public Func<ScoreType, int, int> EvaluationMethod { get; set; }

    }
}

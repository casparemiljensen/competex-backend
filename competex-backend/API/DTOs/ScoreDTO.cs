using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend.Dtos
{
    public abstract class ScoreDTO
    {
        public MatchDTO? Match { get; set; }
        public Guid? MatchId { get; set; }
        public ParticipantDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
        public ScoreType ScoreType { get; set; }
        public List<PenaltyDTO>? Penalties { get; set; } = new List<PenaltyDTO>();
        public List<Guid>? PenaltyIds { get; set; } = new List<Guid>();
        public abstract object ScoreValue { get; set; }
    }

    public class TimeScoreDTO : ScoreDTO
    {
        public TimeSpan Time { get; set; }

        public override object ScoreValue
        {
            get { return Time; }
            set { Time = (TimeSpan)value; }
        }
    }

    public class SetScoreDTO : ScoreDTO
    {
        public int SetsWon { get; set; }

        public override object ScoreValue
        {
            get { return SetsWon; }
            set { SetsWon = (int)value; }
        }
    }

    public class PointScoreDTO : ScoreDTO
    {
        public int Points { get; set; }

        public override object ScoreValue
        {
            get { return Points; }
            set { Points = (int)value; }
        }
    }
}

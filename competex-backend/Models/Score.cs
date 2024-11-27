namespace competex_backend.Models
{
    public abstract class Score : Identifiable
    {

        public Guid MatchId { get; set; }
        public Guid ParticipantId { get; set; }
        public ScoreType ScoreType { get; set; }
        // Keeping ScoreValue as a protected abstract property for derived classes
        public abstract object ScoreValue { get; set; }
        //public Judge? JudgedBy { get; set; }
        public List<Guid> PenaltyIds { get; set; } = new List<Guid>();

        public Score(Guid matchId, Guid participantId, ScoreType scoreType)
        {
            MatchId = matchId;
            ParticipantId = participantId;
            ScoreType = scoreType;
        }
    }


    public class TimeScore : Score
    {
        public TimeSpan Time { get; set; }
        public TimeScore(TimeSpan time, Guid matchId, Guid participantId) : base(matchId, participantId, ScoreType.Time)
        {
            Time = time;
        }

        public override object ScoreValue
        {
            get { return Time; }
            set { Time = (TimeSpan)value; }  // Ensure it's a TimeSpan when setting
        }
    }

    public class SetScore : Score
    {
        // Number of sets won by the participant
        public int SetsWon { get; set; }

        // Constructor
        public SetScore(int setsWon, Guid matchId, Guid participantId) : base(matchId, participantId, ScoreType.Set)
        {
            this.SetsWon = setsWon;
        }

        // Override ScoreValue to return and set the number of sets won
        public override object ScoreValue
        {
            get { return SetsWon; }
            set { SetsWon = (int)value; }  // Ensure it's an int when setting
        }
    }

    public class PointScore : Score
    {
        public int Points { get; set; }
        public PointScore(int points, Guid matchId, Guid participantId) : base(matchId, participantId, ScoreType.Number)
        {
            Points = points;
        }

        // Override ScoreValue to return the specific Points value
        public override object ScoreValue
        {
            get { return Points; }
            set { Points = (int)value; }  // Ensure it's an int when setting
        }
    }
}
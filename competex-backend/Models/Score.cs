namespace competex_backend.Models
{
    public abstract class Score : Identifiable
    {
        public Guid MatchId { get; set; }
        public Guid ParticipantId { get; set; }
        //public ScoreType ScoreType { get; set; }
        // Keeping ScoreValue as a protected abstract property for derived classes
        public abstract object ScoreValue { get; set; }
        //public Judge? JudgedBy { get; set; }
        public List<Guid> PenaltyIds { get; set; } = new List<Guid>();

        public Score(Guid matchId, Guid participantId)
        {
            MatchId = matchId;
            ParticipantId = participantId;
        }
    }

    // Overvej at lave Score til en generic


    public class TimeScore : Score
    {
        public TimeSpan Time { get; set; }
        public TimeScore(TimeSpan time, Guid matchId, Guid participantId) : base(matchId, participantId)
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
        public SetScore(int setsWon, Guid matchId, Guid participantId) : base(matchId, participantId)
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
        public PointScore(int points, Guid matchId, Guid participantId) : base(matchId, participantId)
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

    public class TimeFaultScore : Score
    {
        public int Faults { get; set; }
        public DateTime Time { get; set; }
        public TimeFaultScore(int faults, DateTime time, Guid matchId, Guid participantId) : base(matchId, participantId)
        {
            Faults = faults;
            Time = time;
        }

        // Override ScoreValue to return the specific Faults value
        public override object ScoreValue
        {
            get { return (Faults, Time); }
            set
            {
                if (value is ValueTuple<int, DateTime> tuple) // Check if value is a tuple of the correct type
                {
                    Faults = tuple.Item1; // Extract Faults
                    Time = tuple.Item2;   // Extract Time
                }
                else
                {
                    throw new ArgumentException("Value must be a tuple of type (int, DateTime).");
                }
            }
        }
    }
}
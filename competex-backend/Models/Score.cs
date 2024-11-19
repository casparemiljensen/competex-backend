namespace competex_backend.Models
{
    public abstract class Score : Identifiable
    {

        public Match Match { get; set; }
        public Participant Participant { get; set; }
        public ScoreType ScoreType { get; set; }
        // Keeping ScoreValue as a protected abstract property for derived classes
        public abstract object ScoreValue { get; set; }
        //public Judge? JudgedBy { get; set; }
        public List<Penalty> Penalties { get; set; } = new List<Penalty>();

        public Score(Match match, Participant participant, ScoreType scoreType)
        {
            Match = match;
            Participant = participant;
            ScoreType = scoreType;
        }
    }


    public class TimeScore : Score
    {
        public TimeSpan Time { get; set; }
        public TimeScore(TimeSpan time, Match match, Participant participant) : base(match, participant, ScoreType.Time)
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
        public SetScore(int setsWon, Match match, Participant participant) : base(match, participant, ScoreType.Set)
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
        public PointScore(int points, Match match, Participant participant) : base(match, participant, ScoreType.Number)
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
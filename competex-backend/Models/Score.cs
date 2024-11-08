namespace competex_backend.Models
{
    public abstract class Score : IIdentifiable
    {

        public Guid Id { get; set; }
        public Match? Match { get; set; }
        public Participant? Participant { get; set; }
        public ScoreType ScoreType { get; set; }
        // Keeping ScoreValue as a protected abstract property for derived classes
        public abstract object ScoreValue { get; set; }
        //object directly is not ideal since you’ll lose type safety.
        public Judge? JudgedBy { get; set; }
        public List<Penalty> Penalties { get; set; } = new List<Penalty>();

        public Score(ScoreType scoreType)
        {
            ScoreType = scoreType;
        }
    }


    public class TimeScore : Score
    {
        public TimeSpan Time { get; set; }
        public TimeScore(TimeSpan time) : base(ScoreType.Time)
        {
            Time = time;
        }

        public override object ScoreValue
        {
            get { return Time; }
            set { Time = (TimeSpan)value; }  // Ensure it's a TimeSpan when setting
        }

        public class SetScore : Score
        {
            // Number of sets won by the participant
            public int SetsWon { get; set; }

            // Constructor
            public SetScore(int setsWon) : base(ScoreType.Set)
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
            public PointScore(int points) : base(ScoreType.Number)
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
}
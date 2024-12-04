using Npgsql;

namespace competex_backend.Models
{
    public abstract class Score : Identifiable, IMappable<Score>
    {
        public Guid MatchId { get; set; }
        public Guid ParticipantId { get; set; }
        //public ScoreType ScoreType { get; set; }
        // Keeping ScoreValue as a protected abstract property for derived classes
        public abstract object ScoreValue { get; set; }
        //public Judge? JudgedBy { get; set; }
        public List<Penalty> PenaltyIds { get; set; } = [];

        public Score(Guid matchId, Guid participantId)
        {
            MatchId = matchId;
            ParticipantId = participantId;
        }

        public static Task<Score> Map(NpgsqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Tuple<List<string>, List<NpgsqlParameter>> GetInsertSQLObject()
        {
            throw new NotImplementedException();
        }
    }


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
}
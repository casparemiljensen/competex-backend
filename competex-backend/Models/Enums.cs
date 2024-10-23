namespace competex_backend.Models
{
    public enum MatchStatus
    {
        pending,
        active,
        cancelled,
        concluded
    }

    public enum SurfaceType
    {
        unknown,
        natural_grass,
        artificial_turf,
        clay,
        dirt,
        turf
    }

    public enum EntityType
    {
        rabbit,
        horse
    }

    public enum EntityLevel
    {
        intermediate,
        beginner,
        advanced
    }

    public enum PenaltyType
    {
        time,
        distance,
        points
    }

    public enum ScoreType
    {
        time,
        set,
        number
    }
}

namespace competex_backend.Models
{
    public enum MatchStatus
    {
        Pending,
        Active,
        Cancelled,
        Concluded
    }

    public enum SurfaceType
    {
        Unknown,
        NaturalGrass,
        ArtificialTurf,
        Clay,
        Dirt,
        Turf
    }

    public enum ClubMemberRole
    {
        Standard,
        Organizer
    }

    public enum EntityType
    {
        Rabbit,
        Horse
    }

    public enum EntityLevel
    {
        Intermediate,
        Beginner,
        Advanced
    }

    public enum PenaltyType
    {
        Time,
        Distance,
        Points
    }

    public enum ScoreType
    {
        Time,
        Set,
        Number
    }
    public enum RoundType
    {
        Base,
        Middle,
        Final,
    }

    public enum RoundStatus
    {
        Future,
        Starting,
        Ongoing,
        Ended
    }
}

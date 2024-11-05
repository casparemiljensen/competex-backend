namespace competex_backend.Models
{
    public enum Status // Can be used for both competition and event.
    {
        Pending,
        Active,
        Cancelled,
        Concluded
    }

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

    public enum Level // Can be used for all types of participants and competitions.
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
        Number,
        TimeAndPenalty
    }

    public enum ScoreMethod
    {
        D1, // 2 Rounds
        C2 // Samlet tid
    }
}

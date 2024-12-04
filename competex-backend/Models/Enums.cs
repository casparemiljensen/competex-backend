namespace competex_backend.Models
{
    public enum Status // Can be used for both competition and event.
    {
        Pending,
        Active,
        Cancelled,
        Concluded
    }

    public enum RegistrationStatus
    {
        PendingPayment,
        Paid,
        Accepted
    }

    public enum MatchStatus
    {
        Pending,
        Active,
        Cancelled,
        Concluded
    }

    public enum ParticipantType
    {
        Single,
        Team,
        Ekvipage
    }

    public enum SurfaceType
    {
        Unknown,
        NaturalGrass,
        ArtificialTurf,
        Clay,
        Dirt,
        Turf,
        PVC,
    }

    public enum ClubMemberRole
    {
        Standard,
        Organizer
    }

    public enum EntityType
    {
        Rabbit,
        Horse,
        None
    }

    public enum Level // Can be used for all types of participants and competitions.
    {
        Intermediate,
        Beginner,
        Advanced,
        Professional
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
        C2, // Samlet tid
        None
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

    public enum JudgeType
    {
        Main,
        Assistant
    }

    [Flags]
    public enum Permissions
    {
        User = 1,
        Member = 2,
        Admin = 4,
        Owner = 8
    }
}

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
}

namespace competex_backend.Models
{
    public class Field : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? Surface { get; set; } = SurfaceType.Unknown;
    }
}
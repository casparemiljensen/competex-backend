namespace competex_backend.Models
{
    public class Field : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? SurfaceType { get; set; } = Models.SurfaceType.Unknown;
    }
}
using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class FieldDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public SurfaceType? SurfaceType { get; set; } = Models.SurfaceType.Unknown;
    }
}

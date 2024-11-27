using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class AdminDTO : Identifiable
    {
        public List<SportType>? SportTypes { get; set; }
        public List<Guid>? SportTypeIds { get; set; }
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class AdminDTO
    {
        public Guid Id { get; set; }
        public required List<SportType> SportTypes { get; set; }
    }
}

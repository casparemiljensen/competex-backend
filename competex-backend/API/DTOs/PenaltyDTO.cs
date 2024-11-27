using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class PenaltyDTO
    {
        public Guid Id { get; set; }
        public PenaltyType PenaltyType { get; set; }
        public object? PenaltyValue { get; set; }
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class PenaltyDTO : Identifiable
    {
        public PenaltyType PenaltyType { get; set; }
        public object? PenaltyValue { get; set; }
    }
}

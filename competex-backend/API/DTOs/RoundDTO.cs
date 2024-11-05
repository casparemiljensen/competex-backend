using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class RoundDTO
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public uint SequenceNumber { get; set; }
        public RoundType RoundType { get; set; }
        public Guid CompetitionId { get; init; }
        public RoundStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid[]? Matches { get; set; }
    }
}


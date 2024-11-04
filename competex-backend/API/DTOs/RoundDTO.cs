using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class RoundDTO
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public uint SequenceNumber { get; set; }
        public required RoundTypeEnum RoundType { get; set; }
        public Guid CompetitionId { get; init; }
        public RoundStatusEnum Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required Guid[] Matches { get; set; }
    }
}


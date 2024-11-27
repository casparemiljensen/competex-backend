using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class RoundDTO : Identifiable
    {
        public required string Name { get; set; }
        public uint SequenceNumber { get; set; }
        public RoundType RoundType { get; set; }
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; init; }
        public RoundStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public IEnumerable<MatchDTO>? Matches { get; set; }
        //public IEnumerable<Guid>? MatchIds { get; set; }
    }
}


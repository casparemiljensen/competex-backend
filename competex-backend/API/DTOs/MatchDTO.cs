using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class MatchDTO : Identifiable
    {
        public RoundDTO? Round { get; set; }
        public Guid? RoundId { get; set; }
        public List<EkvipageDTO>? Participants { get; set; }
        public List<Guid>? ParticipantIds { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public FieldDTO? Field { get; set; }
        public Guid? FieldId { get; set; }
        public JudgeDTO? Judge { get; set; }
        public Guid? JudgeId { get; set; }
    }
}

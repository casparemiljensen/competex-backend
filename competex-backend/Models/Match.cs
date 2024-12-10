using competex_backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace competex_backend.Models
{
    public class Match : Identifiable
    {
        public Guid RoundId { get; set; }
        public List<Guid>? ParticipantIds { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? JudgeId { get; set; }

        // Navigation property for many-to-many relationship
        [NotMapped]
        public ICollection<Ekvipage> Ekvipages { get; set; } = new List<Ekvipage>();
    }
}

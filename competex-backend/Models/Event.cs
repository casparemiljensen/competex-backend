
using System.ComponentModel.DataAnnotations.Schema;

namespace competex_backend.Models
{
    public class Event : Identifiable
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? LocationId { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public required Guid OrganizerId { get; set; } // ClubId
        public required Guid SportTypeId { get; set; }
        [NotMapped]
        public List<Guid> CompetitionIds { get; set; } = [];
        public int EntryFee { get; set; } = 0;
        public Result AddCompetition(Guid competitionId)
        {
            CompetitionIds.Add(competitionId);
            return Result.Success();
        }
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class EventDTO : Identifiable
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocationDTO? Location { get; set; }
        public Guid? LocationId { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public required Guid Organizer { get; set; } // ClubId - We need more info than this in frontend...
        public SportTypeDTO? SportType { get; set; }
        public Guid? SportTypeId { get; set; }
        public List<CompetitionDTO>? Competitions { get; set; }
        public List<Guid>? CompetitionIds { get; set; } = [];
        public int EntryFee { get; set; } = 0;

    }
}
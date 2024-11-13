namespace competex_backend.Models
{
    public class Event : Identifiable
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Location? Location { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public required Guid Organizer { get; set; } // ClubId
        public SportType SportType { get; set; }
        public List<Competition>? Competitions { get; set; }
    }
}

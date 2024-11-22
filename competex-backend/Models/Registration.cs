namespace competex_backend.Models
{
    public class Registration : Identifiable
    {
        public Guid MemberId { get; set; }
        public Guid CompetitionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }
}

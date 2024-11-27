namespace competex_backend.Models
{
    public class Registration : Identifiable
    {
        public Member? Member { get; set; }
        public Guid? MemberId { get; set; }
        public Competition? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }
}

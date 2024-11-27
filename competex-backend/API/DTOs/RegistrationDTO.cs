using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class RegistrationDTO : Identifiable
    {
        public MemberDTO? Member { get; set; }
        public Guid? MemberId { get; set; }
        public CompetitionDTO? Competition { get; set; }
        public Guid? CompetitionId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }
}

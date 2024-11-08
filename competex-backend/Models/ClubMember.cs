using Microsoft.Extensions.Hosting;

namespace competex_backend.Models
{
    public class ClubMember : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid ClubId { get; set; }
        public Club Club { get; set; } = null!;
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public ClubMemberRole? Role { get; set; } = ClubMemberRole.Standard;

    }
}

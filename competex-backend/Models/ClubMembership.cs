using Microsoft.Extensions.Hosting;

namespace competex_backend.Models
{
    public class ClubMembership : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid ClubId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public ClubMemberRole? Role { get; set; } = ClubMemberRole.Standard;
    }
}

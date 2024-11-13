using competex_backend.Models;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    public class ClubMembershipDTO
    {
        public Guid Id { get; init; }
        public Guid ClubId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime JoinDate { get; set; }
        public ClubMemberRole? Role { get; set; } = ClubMemberRole.Standard;
    }
}

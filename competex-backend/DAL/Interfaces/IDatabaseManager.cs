using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IDatabaseManager
    {
        List<Club> Clubs { get; set; }
        List<Member> Members { get; set; }
        List<ClubMember> ClubMembers { get; set; }
        List<Entity> Entities { get; set; }
    }
}

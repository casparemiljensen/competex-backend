using competex_backend.Models;

namespace competex_backend.DataAccess.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
    }
}

using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
    }
}

using competex_backend.Models;
using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        List<Member> IMemberRepository.GetMembers()
        {
            return new List<Member>
            {
                new Member("John Doe"),
                new Member("Jane Doe"),
                new Member("Alice Doe")
            };
        }
    }
}

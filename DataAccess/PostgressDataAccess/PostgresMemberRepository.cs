using competex_backend.Models;
using competex_backend.DataAccess.Interfaces;

namespace competex_backend.DataAccess.MockDataAccess
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

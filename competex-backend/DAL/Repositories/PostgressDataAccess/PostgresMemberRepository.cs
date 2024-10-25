using competex_backend.Models;
using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        public Task<bool> AddMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMemberAsync(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public Task<Member?> GetMemberByIdAsync(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetMembersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }
    }
}

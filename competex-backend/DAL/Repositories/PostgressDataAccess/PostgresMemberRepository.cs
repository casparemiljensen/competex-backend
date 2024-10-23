using competex_backend.Models;
using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        public void AddMember(Member member)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public Member GetMemberById(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }

        List<Member> IMemberRepository.GetMembers()
        {
            throw new NotImplementedException();
        }
    }
}

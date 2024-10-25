using competex_backend.Models;
using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        public Member GetMemberById(Guid memberId)
        {
            throw new NotImplementedException();
        }

        bool IMemberRepository.AddMember(Member member)
        {
            throw new NotImplementedException();
        }

        bool IMemberRepository.DeleteMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        List<Member> IMemberRepository.GetMembers()
        {
            throw new NotImplementedException();
        }

        bool IMemberRepository.UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}

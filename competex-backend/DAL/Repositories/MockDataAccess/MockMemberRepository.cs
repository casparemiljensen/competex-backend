using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using AutoMapper.Execution;
using Member = competex_backend.Models.Member;
using System.Numerics;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    internal class MockMemberRepository : IMemberRepository
    {
        private List<Member> _members;

        public MockMemberRepository()
        {
            _members = new List<Member>();
        }

        public void AddMember(Member member)
        {
            member.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            _members.Add(member);
        }


        public void DeleteMember(Guid memberId)
        {
            var memberToRemove = _members.FirstOrDefault(m => m.MemberId == memberId);
            if (memberToRemove != null)
            {
                _members.Remove(memberToRemove);
            }
        }

        public Member GetMemberById(Guid memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId) ?? throw new Exception("No member found");
        }

        public void UpdateMember(Member member)
        {
            var existingMember = _members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if (existingMember != null)
            {
                existingMember.FirstName = member.FirstName;
                existingMember.LastName = member.LastName;
                existingMember.Birthday = member.Birthday;
                existingMember.Email = member.Email;
                existingMember.Phone = member.Phone;
                existingMember.Permissions = member.Permissions;
            }
        }

        List<Member> IMemberRepository.GetMembers()
        {
            return _members;
        }


    }
}

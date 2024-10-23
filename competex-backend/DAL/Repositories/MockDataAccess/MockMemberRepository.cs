using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using AutoMapper.Execution;
using Member = competex_backend.Models.Member;
using System.Numerics;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockMemberRepository : IMemberRepository
    {
        private readonly IDatabaseManager _db;

        public MockMemberRepository(IDatabaseManager db)
        {
            _db = db;
        }

        public void AddMember(Member member)
        {
            member.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            _db.Members.Add(member);
        }


        public void DeleteMember(Guid memberId)
        {
            var memberToRemove = _db.Members.FirstOrDefault(m => m.MemberId == memberId);
            if (memberToRemove != null)
            {
                _db.Members.Remove(memberToRemove);
            }
        }

        public Member GetMemberById(Guid memberId)
        {
            return _db.Members.FirstOrDefault(m => m.MemberId == memberId) ?? throw new Exception("No member found");
        }

        public void UpdateMember(Member member)
        {
            var existingMember = _db.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
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
            return _db.Members;
        }


    }
}

using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using AutoMapper.Execution;
using Member = competex_backend.Models.Member;
using System.Numerics;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    internal class MockMemberRepository : IMemberRepository
    {
        private List<Member> _members;

        public MockMemberRepository()
        {
            _members = new List<Member>
            {
                new Member("Caspar", "Emil Jensen", new Guid("bec52019-b429-47bc-987e-47d13224d75e")) { Birthday = new DateTime(1990, 1, 1), Email = "Caspar@uni.com", Phone = "12345890", Permissions = "Admin" },
                new Member("Thomas", "Ilum Andersen", new Guid("cd4d665d-cd71-4aaa-9799-9f9c973ce19e")) { Birthday = new DateTime(1985, 5, 23), Email = "Ilum@uni.com", Phone = "98763210", Permissions = "User" },
                new Member("Thomas", "Dam Nykjær", new Guid("c7a53ea7-950a-4c8f-83c8-6262f2ec1571")) { Birthday = new DateTime(1995, 10, 10), Email = "Dam@uni.com", Phone = "55555555", Permissions = "Judge" }
            };
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

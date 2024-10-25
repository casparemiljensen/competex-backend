using competex_backend.DAL.Interfaces;
using Member = competex_backend.Models.Member;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockMemberRepository : IMemberRepository
    {
        private readonly IDatabaseManager _db;

        public MockMemberRepository(IDatabaseManager db)
        {
            _db = db;
        }

        List<Member> IMemberRepository.GetMembers()
        {
            return _db.Members;
        }

        public bool AddMember(Member member)
        {
            member.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                _db.Members.Add(member);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteMember(Guid memberId)
        {
            var memberToRemove = _db.Members.FirstOrDefault(m => m.MemberId == memberId);
            if (memberToRemove != null)
            {
                try
                {
                    _db.Members.Remove(memberToRemove);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not delete member", ex);
                }
            }
            return false;
        }


        public Member GetMemberById(Guid memberId)
        {
            return _db.Members.FirstOrDefault(m => m.MemberId == memberId) ?? throw new Exception("No member found");
        }

        public bool UpdateMember(Member member)
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
                return false;
            }
            return false;
        }
    }
}

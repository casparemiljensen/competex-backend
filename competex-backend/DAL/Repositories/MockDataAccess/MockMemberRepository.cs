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

        public Member? GetById(Guid id)
        {
            return _db.Members.FirstOrDefault(m => m.MemberId == id) ?? throw new Exception("No member found");
        }


        public IEnumerable<Member> GetAll()
        {
            return _db.Members;
        }

        public Guid Insert(Member obj)
        {
            obj.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                _db.Members.Add(obj);
                return obj.MemberId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public bool Update(Member obj)
        {
            var existingMember = _db.Members.FirstOrDefault(m => m.MemberId == obj.MemberId);
            if (existingMember != null)
            {
                existingMember.FirstName = obj.FirstName;
                existingMember.LastName = obj.LastName;
                existingMember.Birthday = obj.Birthday;
                existingMember.Email = obj.Email;
                existingMember.Phone = obj.Phone;
                existingMember.Permissions = obj.Permissions;
                return false;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            var memberToRemove = _db.Members.FirstOrDefault(m => m.MemberId == id);
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
    }
}

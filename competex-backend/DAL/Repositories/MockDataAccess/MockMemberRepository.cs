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

        // Retrieve a specific member by ID asynchronously
        public Task<Member?> GetByIdAsync(Guid Id)
        {
            var member = _db.Members.FirstOrDefault(m => m.MemberId == Id);
            return Task.FromResult(member);
        }

        // Retrieve all members asynchronously
        public Task<IEnumerable<Member>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Member>>(_db.Members.AsEnumerable());
        }



        // Add a new member asynchronously
        public async Task<Guid> InsertAsync(Member obj)
        {
            obj.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                _db.Members.Add(obj);
                await Task.CompletedTask; // Simulate async work
                return obj.MemberId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }


        // Update an existing member asynchronously
        public async Task<bool> UpdateAsync(Member obj)
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
                await Task.CompletedTask; // Simulate async work
                return true;
            }
            return false;
        }


        // Delete a member by ID asynchronously
        public async Task<bool> DeleteAsync(Guid id)
        {
            var memberToRemove = _db.Members.FirstOrDefault(m => m.MemberId == id);
            if (memberToRemove != null)
            {
                try
                {
                    _db.Members.Remove(memberToRemove);
                    await Task.CompletedTask; // Simulate async work
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

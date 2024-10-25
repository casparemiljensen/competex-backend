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

        // Retrieve all members asynchronously
        public Task<List<Member>> GetMembersAsync()
        {
            return Task.FromResult(_db.Members);
        }

        // Retrieve a specific member by ID asynchronously
        public Task<Member?> GetMemberByIdAsync(Guid memberId)
        {
            var member = _db.Members.FirstOrDefault(m => m.MemberId == memberId);
            return Task.FromResult(member);
        }

        // Add a new member asynchronously
        public async Task<bool> AddMemberAsync(Member member)
        {
            member.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                _db.Members.Add(member);
                await Task.CompletedTask; // Simulate async work
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Delete a member by ID asynchronously
        public async Task<bool> DeleteMemberAsync(Guid memberId)
        {
            var memberToRemove = _db.Members.FirstOrDefault(m => m.MemberId == memberId);
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

        // Update an existing member asynchronously
        public async Task<bool> UpdateMemberAsync(Member member)
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
                await Task.CompletedTask; // Simulate async work
                return true;
            }
            return false;
        }
    }
}

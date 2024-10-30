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
        public async Task<ResultT<Member>> GetByIdAsync(Guid id)
        {
            var member = await Task.Run(() => _db.Members.FirstOrDefault(m => m.MemberId == id));
            return member is not null
                ? ResultT<Member>.Success(member)
                : ResultT<Member>.Failure(Error.NotFound("Member not found.", $"Member with ID {id} does not exist."));
        }

        // Retrieve all members asynchronously
        public async Task<ResultT<IEnumerable<Member>>> GetAllAsync()
        {
            var members = await Task.Run(() => _db.Members.AsEnumerable());
            return ResultT<IEnumerable<Member>>.Success(members);
        }


        // Add a new member asynchronously
        public async Task<ResultT<Guid>> InsertAsync(Member obj)
        {
            obj.MemberId = Guid.NewGuid();  // Generate a new Guid for new members
            try
            {
                await Task.Run(() => _db.Members.Add(obj)); // Simulate async work
                return ResultT<Guid>.Success(obj.MemberId);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("InsertionError", $"Failed to insert member: {ex.Message}"));
            }
        }


        // Update an existing member asynchronously
        public async Task<Result> UpdateAsync(Member obj)
        {
            int index = await Task.Run(() => _db.Members.FindIndex(m => m.MemberId == obj.MemberId));
            if (index == -1)
            {
                return Result.Failure(Error.NotFound("MemberNotFound", $"Member with ID {obj.MemberId} does not exist."));
            }

            try
            {
                await Task.Run(() =>
                {
                    _db.Members[index] = obj; // Replace the object directly
                }); // Simulate async work

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UpdateError", $"Failed to update member: {ex.Message}"));
            }
        }


        // Delete a member by ID asynchronously
        public async Task<Result> DeleteAsync(Guid id)
        {
            var memberToRemove = await Task.Run(() => _db.Members.FirstOrDefault(m => m.MemberId == id));
            if (memberToRemove is null)
            {
                return Result.Failure(Error.NotFound("MemberNotFound", $"Member with ID {id} does not exist."));
            }

            try
            {
                await Task.Run(() => _db.Members.Remove(memberToRemove)); // Simulate async work
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Could not delete member: {ex.Message}"));
            }
        }
    }
}

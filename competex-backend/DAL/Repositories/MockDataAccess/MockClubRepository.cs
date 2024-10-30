using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubRepository : IClubRepository
    {
        private readonly IDatabaseManager _db;

        public MockClubRepository(IDatabaseManager db)
        {
            _db = db;
        }

        // Get club by ID
        public async Task<ResultT<Club>> GetByIdAsync(Guid clubId)
        {
            var club = await Task.Run(() => _db.Clubs.FirstOrDefault(c => c.ClubId == clubId));
            return club is not null
                ? ResultT<Club>.Success(club)
                : ResultT<Club>.Failure(Error.NotFound("Club not found.", $"Club with ID {clubId} does not exist."));
            //return await Task.FromResult(club);
        }

        // Get all clubs
        public async Task<ResultT<IEnumerable<Club>>> GetAllAsync()
        {
            var clubs = await Task.Run(() => _db.Clubs.ToList());
            return ResultT<IEnumerable<Club>>.Success(clubs);
        }


        // Add a new club
        public async Task<ResultT<Guid>> InsertAsync(Club obj)
        {
            obj.ClubId = Guid.NewGuid();  // Generate a new Guid for new clubs
            try
            {
                await Task.Run(() => _db.Clubs.Add(obj)); // Simulate async work
                return ResultT<Guid>.Success(obj.ClubId);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("InsertionError", $"Failed to insert club: {ex.Message}"));
            }
        }


        // Update an existing club
        public async Task<Result> UpdateAsync(Club obj)
        {
            int index = await Task.Run(() => _db.Clubs.FindIndex(m => m.ClubId == obj.ClubId));
            if (index == -1)
            {
                return Result.Failure(Error.NotFound("ClubNotFound", $"Club with ID {obj.ClubId} does not exist."));
            }

            try
            {
                await Task.Run(() =>
                {
                    _db.Clubs[index] = obj; // Replace the object directly
                }); // Simulate async work

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("UpdateError", $"Failed to update club: {ex.Message}"));
            }
        }

        // Delete a club
        public async Task<Result> DeleteAsync(Guid id)
        {
            var clubToRemove = await Task.Run(() => _db.Clubs.FirstOrDefault(c => c.ClubId == id));
            if (clubToRemove is null)
            {
                return Result.Failure(Error.NotFound("ClubNotFound", $"Club with ID {id} does not exist."));
            }

            try
            {
                await Task.Run(() => _db.Clubs.Remove(clubToRemove)); // Simulate async work
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Could not delete club: {ex.Message}"));
            }
        }

        public async Task<ResultT<IEnumerable<Club>>> GetClubsByNameAsync(string name)
        {
            // Simulate an asynchronous operation
            var clubs = await Task.Run(() => _db.Clubs.Where(c => c.Name == name).ToList());

            // Return the results wrapped in ResultT
            return ResultT<IEnumerable<Club>>.Success(clubs);
        }
    }
}

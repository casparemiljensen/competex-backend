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
        public async Task<Club?> GetByIdAsync(Guid clubId)
        {
            var club = _db.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            return await Task.FromResult(club);
        }

        // Get all clubs
        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            return await Task.FromResult(_db.Clubs);
        }


        // Add a new club
        public async Task<Guid> InsertAsync(Club obj)
        {
            obj.ClubId = Guid.NewGuid();  // Generate a new Guid for new clubs
            try
            {
                _db.Clubs.Add(obj);
                await Task.CompletedTask;
                return obj.ClubId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
            
        }


        // Update an existing club
        public async Task<bool> UpdateAsync(Club obj)
        {
            var existingClub = _db.Clubs.FirstOrDefault(c => c.ClubId == obj.ClubId);
            if (existingClub != null)
            {
                existingClub.Name = obj.Name;
                existingClub.AssociatedSport = obj.AssociatedSport;
                await Task.CompletedTask; // Simulate async work
                return true;
                //existingClub.Organizers = club.Organizers;
                //existingClub.ClubMembers = club.ClubMembers;
            }
            return false;
        }

        // Delete a club
        public async Task<bool> DeleteAsync(Guid id)
        {
            var clubToRemove = _db.Clubs.FirstOrDefault(c => c.ClubId == id);    
            if (clubToRemove != null)
            {
                try
                {
                    _db.Clubs.Remove(clubToRemove);
                    await Task.CompletedTask; // Simulate async work
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not delete club", ex);
                }
            }
            return false;
        }

        public IEnumerable<Club> GetClubByName(string name)
        {
            var clubs = _db.Clubs.Where(c => c.Name == name).ToList();
            return clubs;
        }
    }
}

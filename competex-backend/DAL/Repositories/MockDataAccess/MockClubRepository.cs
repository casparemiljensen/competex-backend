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

        // Get all clubs
        public async Task<List<Club>> GetClubsAsync()
        {
            return await Task.FromResult(_db.Clubs);
        }

        // Get club by ID
        public async Task<Club?> GetClubByIdAsync(Guid clubId)
        {
            var club = _db.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            return await Task.FromResult(club);
        }

        // Add a new club
        public async Task AddClubAsync(Club club)
        {
            club.ClubId = Guid.NewGuid();  // Generate a new Guid for new clubs
            _db.Clubs.Add(club);
            await Task.CompletedTask;
        }

        // Update an existing club
        public async Task UpdateClubAsync(Club club)
        {
            var existingClub = _db.Clubs.FirstOrDefault(c => c.ClubId == club.ClubId);
            if (existingClub != null)
            {
                existingClub.Name = club.Name;
                existingClub.AssociatedSport = club.AssociatedSport;
                //existingClub.Organizers = club.Organizers;
                //existingClub.ClubMembers = club.ClubMembers;
            }
            await Task.CompletedTask;
        }

        // Delete a club
        public async Task DeleteClubAsync(Guid clubId)
        {
            var clubToRemove = _db.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            if (clubToRemove != null)
            {
                _db.Clubs.Remove(clubToRemove);
            }
            await Task.CompletedTask;
        }
    }
}

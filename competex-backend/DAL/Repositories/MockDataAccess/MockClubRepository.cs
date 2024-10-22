using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubRepository : IClubRepository
    {
        private List<Club> _clubs;

        public MockClubRepository()
        {
            // Initialize with some mock data
            _clubs = new List<Club>();
        }

        // Get all clubs
        public List<Club> GetClubs()
        {
            return _clubs;
        }

        // Get club by ID
        public Club GetClubById(Guid clubId)
        {
            return _clubs.FirstOrDefault(c => c.ClubId == clubId);
        }

        // Add a new club
        public void AddClub(Club club)
        {
            club.ClubId = Guid.NewGuid();  // Generate a new Guid for new clubs
            _clubs.Add(club);
        }

        // Update an existing club
        public void UpdateClub(Club club)
        {
            var existingClub = _clubs.FirstOrDefault(c => c.ClubId == club.ClubId);
            if (existingClub != null)
            {
                existingClub.Name = club.Name;
                existingClub.AssociatedSport = club.AssociatedSport;
                existingClub.Organizers = club.Organizers;
                existingClub.ClubMembers = club.ClubMembers;
            }
        }

        // Delete a club
        public void DeleteClub(Guid clubId)
        {
            var clubToRemove = _clubs.FirstOrDefault(c => c.ClubId == clubId);
            if (clubToRemove != null)
            {
                _clubs.Remove(clubToRemove);
            }
        }
    }
}

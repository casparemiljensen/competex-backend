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

        public Club? GetById(Guid clubId)
        {
            return _db.Clubs.FirstOrDefault(c => c.ClubId == clubId);
        }

        public IEnumerable<Club> GetAll()
        {
            return _db.Clubs;
        }

        public Guid Insert(Club club)
        {
            club.ClubId = Guid.NewGuid();  // Generate a new Guid for new clubs
            _db.Clubs.Add(club);
            // TODO: Figure out where to implement Guid creation.
            return club.ClubId;
        }

        public bool Update(Club club)
        {
            var existingClub = _db.Clubs.FirstOrDefault(c => c.ClubId == club.ClubId);
            if (existingClub != null)
            {
                existingClub.Name = club.Name;
                existingClub.AssociatedSport = club.AssociatedSport;
                return true;
                //existingClub.Organizers = club.Organizers;
                //existingClub.ClubMembers = club.ClubMembers;
            }
            return false;
        }

        public bool Delete(Guid clubId)
        {
            var clubToRemove = _db.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            if (clubToRemove != null)
            {
                _db.Clubs.Remove(clubToRemove);
                return true;
            }
            return false;
        }

        public List<Club> GetClubByName(string name)
        {
            var clubs = _db.Clubs.Where(c => c.Name == name).ToList();
            return clubs;
        }
    }
}

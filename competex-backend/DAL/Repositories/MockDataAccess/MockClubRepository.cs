using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockClubRepository
    {
        private List<Club> _clubs;

        public MockClubRepository()
        {
            // Initialize with some mock data
            _clubs = new List<Club>
        {
            new Club("Kaninernes Klub Hjørring", "Kaninhop") { Organizers = new List<Member> { new Member("Ane", "Svendsen"), new Member("Irma", "Johnson") }, Users = new List<Member> { new Member("Svend", "Åge"), new Member("Johnny", "Madsen") } },
            new Club("Aabybro kaninhop", "Kaninhop") { Organizers = new List<Member> { new Member("Jørgen", "Larsen"), new Member("Kasper", "Kronjuvel") }, Users = new List<Member> { new Member("Lars", "Lægaard"), new Member("Camilla", "Frydensten") } },
            new Club("Aalborg kaninforening", "Kaninhop") { Organizers = new List<Member> { new Member("Lotte", "Svenstrup"), new Member("Bo", "Gade") }, Users = new List<Member> { new Member("Villy", "Søvnig"), new Member("Jeppe", "Kofod") } },
        };
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
                existingClub.Users = club.Users;
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

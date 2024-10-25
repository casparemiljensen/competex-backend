using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubRepository
    {
        List<Club> GetClubs();
        Club? GetClubById(Guid clubId);
        void AddClub(Club club);
        void UpdateClub(Club club);
        void DeleteClub(Guid clubId);
    }
}

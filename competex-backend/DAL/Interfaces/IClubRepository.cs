using competex_backend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubRepository
    {
        List<Club> GetClubs();
        Club GetClubById(Guid clubId);
        void AddClub(Club club);
        void UpdateClub(Club club);
        void DeleteClub(Guid clubId);
    }
}

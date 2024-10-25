using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubRepository
    {
        Task<List<Club>> GetClubsAsync();
        Task<Club?> GetClubByIdAsync(Guid clubId);
        Task AddClubAsync(Club club);
        Task UpdateClubAsync(Club club);
        Task DeleteClubAsync(Guid clubId);
    }
}

using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubRepository : IGenericRepository<Club>
    {
        List<Club> GetClubByName(string name);
    }
}

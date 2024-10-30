using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IClubRepository : IGenericRepository<Club>
    {
        Task<ResultT<IEnumerable<Club>>> GetClubsByNameAsync(string name);
    }
}

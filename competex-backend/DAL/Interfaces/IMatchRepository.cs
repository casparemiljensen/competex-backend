using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMatchRepository : IGenericRepository<Match>
    {
        Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber);
    }
}

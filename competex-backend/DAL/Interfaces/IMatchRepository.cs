using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMatchRepository : IGenericRepository<Match>
    {
        Task<ResultT<Match>> GetByIdAsync(Guid id);
        Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetAllAsync(int? pageSize, int? pageNumber);
        Task<ResultT<Guid>> InsertAsync(Match entity);
        Task<Result> DeleteAsync(Guid id);
        Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber);

    }
}

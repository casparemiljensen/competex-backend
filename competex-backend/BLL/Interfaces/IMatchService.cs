using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IMatchService : IGenericService<MatchDTO>
    {
        Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> GetMatchesByRoundIdAsync(Guid id, int? pageSize, int? pageNumber);
    }
}

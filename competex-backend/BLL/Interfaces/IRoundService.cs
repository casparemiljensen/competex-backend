using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IRoundService : IGenericService<RoundDTO>
    {
        Task<ResultT<Tuple<int, IEnumerable<RoundDTO>>>> GetByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber);
        Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> CreateMatchesForRoundAsync(Guid competitionId, uint roundSequenceNumber, int? pageSize, int? pageNumber);
    }
}

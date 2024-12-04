using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IScoreService : IGenericService<ScoreDTO>
    {
        Task<ResultT<Tuple<int, IEnumerable<ScoreDTO>>>> GetScoresForParticipant(Guid participantId, int? pageSize, int? pageNumber);

    }
}

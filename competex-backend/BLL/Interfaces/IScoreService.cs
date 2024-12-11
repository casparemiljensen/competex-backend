using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.BLL.Interfaces
{
    public interface IScoreService : IGenericService<ScoreDTO>
    {
        Task<ResultT<Tuple<int, IEnumerable<ScoreDTO>>>> GetScoresForParticipant(Guid participantId, int? pageSize, int? pageNumber);
        public Task<ResultT<PaginationWrapperDTO<IEnumerable<ScoreResultDTO>>>> GetResultsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber);
        public Task<ResultT<IActionResult>> AddPenaltyById(Guid ScoreId, Guid PenaltyId);

    }
}

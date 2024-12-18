using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IRoundAPI : IGenericAPI<RoundDTO>
    {
        Task<ActionResult> GetRoundIdsByCompetitionIdAsync(Guid competitionId, int? pageSize, int? pageNumber);
        Task<IActionResult> CreateMatchesForRoundAsync(Guid competitionId, uint roundSequenceNo, CriteriaDTO? criteria, [FromQuery] int? pageSize, [FromQuery] int? pageNumber);
    }
}
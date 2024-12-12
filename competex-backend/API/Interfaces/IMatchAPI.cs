using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IMatchAPI : IGenericAPI<MatchDTO>
    {
        Task<IActionResult> GetMatchesByRoundIdAsync(Guid id, int? pageSize, int? pageNumber);
    }
}

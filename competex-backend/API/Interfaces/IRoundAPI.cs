using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IRoundAPI : IGenericAPI<RoundDTO>
    {
        // TODO: Change to Task<IActionResult> Or change IGenericAPI to return IActionResult
        IActionResult GetByName(string firstName);

        ActionResult GetRoundIdsByCompetitionId(Guid competitionId);
    }
}

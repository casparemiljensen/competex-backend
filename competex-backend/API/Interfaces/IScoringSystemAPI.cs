using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IScoringSystemAPI : IGenericAPI<ScoringSystemDTO>
    {
        // TODO: Change to Task<IActionResult> Or change IGenericAPI to return IActionResult
    }

}
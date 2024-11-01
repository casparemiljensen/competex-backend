using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IMemberAPI : IGenericAPI<MemberDTO>
    {
        // TODO: Change to Task<IActionResult> Or change IGenericAPI to return IActionResult
        IActionResult GetNumber();

        IActionResult GetByName(string firstName);
    }
}

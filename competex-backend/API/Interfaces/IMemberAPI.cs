using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IMemberAPI : IGenericAPI<MemberDTO>
    {
        IActionResult GetNumber();

        IActionResult GetByName(string firstName);
    }
}

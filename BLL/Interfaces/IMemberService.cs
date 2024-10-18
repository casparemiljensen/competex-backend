using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using competex_backend.Models;
using static System.Net.Mime.MediaTypeNames;
using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IMemberService
    {

        IEnumerable<MemberDTO> GetMembers();

    }
}

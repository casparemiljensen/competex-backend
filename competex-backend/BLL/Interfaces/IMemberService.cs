using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberDTO> GetMembers();
    }
}

using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IMemberService : IGenericService<MemberDTO>
    {
        bool CheckNumber();
        MemberDTO GetByName(string firstName);
    }
}

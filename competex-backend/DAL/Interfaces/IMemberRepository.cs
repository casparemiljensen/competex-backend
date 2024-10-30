using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member?> GetByFirstNameAsync(string firstName);
    }
}

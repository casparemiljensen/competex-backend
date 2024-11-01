using System.Net;

namespace competex_backend.BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> RemoveAsync(Guid id);
    }
}

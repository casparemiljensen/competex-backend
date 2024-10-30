using Common.ResultPattern;
using System.Net;

namespace competex_backend.BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<ResultT<T>> GetByIdAsync(Guid id);
        Task<ResultT<IEnumerable<T>>> GetAllAsync();
        Task<ResultT<Guid>> CreateAsync(T obj);
        Task<Result> UpdateAsync(T obj);
        Task<Result> RemoveAsync(Guid id);
    }
}

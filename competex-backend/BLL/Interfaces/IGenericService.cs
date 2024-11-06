using Common.ResultPattern;
using System.Net;

namespace competex_backend.BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<ResultT<T>> GetByIdAsync(Guid id); //Maybe make T nullable later if we run into problems
        Task<ResultT<IEnumerable<T>>> GetAllAsync(int? pageSize, int? pageNumber);
        Task<ResultT<Guid>> CreateAsync(T obj);
        Task<Result> UpdateAsync(Guid id, T obj);
        Task<Result> RemoveAsync(Guid id);
    }
}

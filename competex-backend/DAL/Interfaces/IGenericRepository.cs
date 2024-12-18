using Common.ResultPattern;
using competex_backend.DAL.Filters;
using competex_backend.Models;
using System.Linq.Expressions;

namespace competex_backend.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ResultT<T>> GetByIdAsync(Guid id);
        Task<ResultT<Tuple<int, IEnumerable<T>>>> GetAllAsync(int? pageSize, int? pageNumber);
        Task<ResultT<Tuple<int, IEnumerable<T>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters);
        Task<ResultT<Guid>> InsertAsync(T obj);
        Task<Result> UpdateAsync(Guid id, T obj);
        Task<Result> DeleteAsync(Guid id);
    }
}

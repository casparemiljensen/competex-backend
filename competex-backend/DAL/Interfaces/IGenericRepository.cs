using Common.ResultPattern;
using competex_backend.DAL.Filters;
using competex_backend.Models;
using System.Linq.Expressions;

namespace competex_backend.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //T? GetById(Guid id);
        //IEnumerable<T> GetAll();
        //Guid Insert(T obj);
        //bool Update(T obj);
        //bool Delete(Guid id);

        Task<ResultT<T>> GetByIdAsync(Guid id);
        Task<ResultT<IEnumerable<T>>> GetAllAsync(int? pageSize, int? pageNumber, BaseFilter? filter = null);
        Task<ResultT<Guid>> InsertAsync(T obj);
        Task<Result> UpdateAsync(Guid id, T obj);
        Task<Result> DeleteAsync(Guid id);
        //Task<ResultT<IEnumerable<T>>> GetByFilterAsync(BaseFilter filter);

    }
}

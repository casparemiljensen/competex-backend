using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //T? GetById(Guid id);
        //IEnumerable<T> GetAll();
        //Guid Insert(T obj);
        //bool Update(T obj);
        //bool Delete(Guid id);

        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Guid> InsertAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> DeleteAsync(Guid id);
    }
}

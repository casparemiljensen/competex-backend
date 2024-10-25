namespace competex_backend.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(Guid id);
        IEnumerable<T> GetAll();
        Guid Insert(T obj);
        bool Update(T obj);
        bool Delete(Guid id);
    }
}

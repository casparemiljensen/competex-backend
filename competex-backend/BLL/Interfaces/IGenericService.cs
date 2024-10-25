using System.Net;

namespace competex_backend.BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        T? GetById(Guid id);
        IEnumerable<T> GetAll();
        bool Create(T obj);
        bool Update(T obj);
        bool Remove(Guid id);
    }
}

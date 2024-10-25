using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IGenericAPI<T>
    {
        IActionResult GetById(Guid id);
        IActionResult GetAll();
        IActionResult Create(T obj);
        IActionResult Update(T obj);
        IActionResult Delete(Guid id);
    }
}

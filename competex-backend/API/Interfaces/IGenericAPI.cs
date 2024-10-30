using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IGenericAPI<T>
    {
        Task<IActionResult> GetById(Guid id);
        Task<IActionResult> GetAll();
        Task<IActionResult> Create(T obj);
        Task<IActionResult> Update(T obj);
        Task<IActionResult> Delete(Guid id);
    }
}

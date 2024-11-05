using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IGenericAPI<T>
    {
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> GetAllAsync();
        Task<IActionResult> CreateAsync(T obj);
        Task<IActionResult> UpdateAsync(Guid id, T obj);
        Task<IActionResult> DeleteAsync(Guid id);
    }
}

using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IGenericAPI<T> where T : class
    {
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> GetAllAsync(int? pageSize, int? pageNumber);
        Task<IActionResult> CreateAsync(T obj);
        Task<IActionResult> UpdateAsync(Guid id, T obj);
        Task<IActionResult> DeleteAsync(Guid id);
    }
}

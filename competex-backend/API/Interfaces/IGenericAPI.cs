using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IGenericAPI<TDto> where TDto : class
    {
        Task<IActionResult> GetByIdAsync(Guid id);
        Task<IActionResult> GetAllAsync(int? pageSize, int? pageNumber);
        Task<IActionResult> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters);
        Task<IActionResult> CreateAsync(TDto obj);
        Task<IActionResult> UpdateAsync(Guid id, TDto obj);
        Task<IActionResult> DeleteAsync(Guid id);
    }
}

using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;

namespace competex_backend.BLL.Services
{
    public class GenericService<T, TDto> : IGenericService<TDto>
        where T : class
        where TDto : class
        // Reference types
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return default(TDto); // Returns the default value for TDto, which is null for reference types and zero/false for value types
            return _mapper.Map<TDto>(entity);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<bool> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _repository.InsertAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

}

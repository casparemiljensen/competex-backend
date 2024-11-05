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


        public async Task<ResultT<TDto>> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result.IsSuccess && result.Value != null)
            {
                return ResultT<TDto>.Success(_mapper.Map<TDto>(result.Value));
            }
            return ResultT<TDto>.Failure(result.Error ?? Error.Failure("UnknownError", "An unknown error occurred."));
        }

        public async Task<ResultT<IEnumerable<TDto>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            var entities = result.Value.Select(m => _mapper.Map<TDto>(m)).ToList();
            return ResultT<IEnumerable<TDto>>.Success(entities);
        }

        public async Task<ResultT<Guid>> CreateAsync(TDto obj)
        {
            var entity = _mapper.Map<T>(obj);
            var result = await _repository.InsertAsync(entity);
            if (result.IsSuccess)
            {
                return ResultT<Guid>.Success(result.Value);
            }
            return ResultT<Guid>.Failure(result.Error ?? Error.Validation("CreationFailed", $"Failed to create {typeof(T)}."));
        }

        public async Task<Result> UpdateAsync(Guid id, TDto obj)
        {
            var entity = _mapper.Map<T>(obj);
            var result = await _repository.UpdateAsync(id, entity);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.Error ?? Error.Validation("UpdateFailed", $"Failed to update {typeof(T)}."));
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.Error ?? Error.Validation("DeletionFailed", $"Failed to delete ${typeof(T)}."));
        }
    }


    //public async Task<TDto?> GetByIdAsync(Guid id)
    //    {
    //        var entity = await _repository.GetByIdAsync(id);
    //        if (entity == null)
    //            return default(TDto); // Returns the default value for TDto, which is null for reference types and zero/false for value types
    //        return _mapper.Map<TDto>(entity);
    //    }

    //    public async Task<IEnumerable<TDto>> GetAllAsync()
    //    {
    //        var entities = await _repository.GetAllAsync();
    //        return _mapper.Map<IEnumerable<TDto>>(entities);
    //    }

    //    public async Task<bool> CreateAsync(TDto dto)
    //    {
    //        var entity = _mapper.Map<T>(dto);
    //        await _repository.InsertAsync(entity);
    //        return true;
    //    }

    //    public async Task<bool> UpdateAsync(TDto dto)
    //    {
    //        var entity = _mapper.Map<T>(dto);
    //        return await _repository.UpdateAsync(entity);
    //    }

    //    public async Task<bool> RemoveAsync(Guid id)
    //    {
    //        return await _repository.DeleteAsync(id);
    //    }
    //}

}

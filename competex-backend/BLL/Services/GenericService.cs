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
        private IClubRepository clubRepo;

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

        public async Task<ResultT<Tuple<int, IEnumerable<TDto>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var result = await _repository.GetAllAsync(pageSize, pageNumber);
            var entities = result.Value.Item2.Select(m => _mapper.Map<TDto>(m)).ToList();
            return ResultT<Tuple<int, IEnumerable<TDto>>>.Success(new Tuple<int, IEnumerable<TDto>>(result.Value.Item1, entities));
        }

        public async Task<ResultT<Tuple<int, IEnumerable<TDto>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            var result = await _repository.SearchAllAsync(pageSize, pageNumber, filters);
            if (!result.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<TDto>>>.Failure(result.Error!);
            }
            var entities = result.Value.Item2.Select(m => _mapper.Map<TDto>(m));
            return ResultT<Tuple<int, IEnumerable<TDto>>>.Success(new Tuple<int, IEnumerable<TDto>>(result.Value.Item1, entities));
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
            var result = await _repository.DeleteAsync(id, false);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.Error ?? Error.Validation("DeletionFailed", $"Failed to delete ${typeof(T)}."));
        }
    }

}

using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Net.Security;

namespace competex_backend.BLL.Services
{
    public class CompetitionService : GenericService<Competition, CompetitionDTO>, ICompetitionService
    {
        private readonly IGenericRepository<Competition> _repository;
        private IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public CompetitionService(IGenericRepository<Competition> repository, IEventRepository eventRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }


        public async override Task<ResultT<Guid>> CreateAsync(CompetitionDTO obj)
        {
            var entity = _mapper.Map<Competition>(obj);
            var result = await _repository.InsertAsync(entity);
            if (result.IsSuccess)
            {
                await _eventRepository.AddCompetition(entity.EventId, entity.Id);
                return ResultT<Guid>.Success(result.Value);
            }

            return ResultT<Guid>.Failure(result.Error ?? Error.Validation("CreationFailed", $"Failed to create {entity.GetType()}."));
        }
    }
}

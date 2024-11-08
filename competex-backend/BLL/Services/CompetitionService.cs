using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class CompetitionService : GenericService<Competition, CompetitionDTO>, ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMapper _mapper;

        public CompetitionService(IGenericRepository<Competition> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _competitionRepository = (ICompetitionRepository)repository;
            _mapper = mapper;
        }
    }
}

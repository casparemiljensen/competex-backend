using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{

    public class MatchService : GenericService<Match, MatchDTO>, IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public MatchService(IGenericRepository<Match> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _matchRepository = (IMatchRepository)repository;
            _mapper = mapper;
        }
    }
}

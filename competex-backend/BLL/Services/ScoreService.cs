using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ScoreService : GenericService<Score, ScoreDTO>, IScoreService
    {
        private readonly IScoreRepository _scoreRepository;
        private readonly IMapper _mapper;

        public ScoreService(IGenericRepository<Score> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _scoreRepository = (IScoreRepository)repository;
            _mapper = mapper;
        }
    }
}

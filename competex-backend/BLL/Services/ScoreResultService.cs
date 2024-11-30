using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ScoreResultService : GenericService<ScoreResult, ScoreResultDTO>, IScoreResultService
    {
        private readonly IScoreResultRepository _scoreResultRepository;
        private readonly IMapper _mapper;

        public ScoreResultService(IGenericRepository<ScoreResult> repository, IMapper mapper) : base(repository, mapper)
        {
            _scoreResultRepository = (IScoreResultRepository)repository;
            _mapper = mapper;
        }
    }
}

using AutoMapper;
using Common.ResultPattern;
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

        public async Task<ResultT<Tuple<int, IEnumerable<ScoreDTO>>>> GetScoresForParticipant(Guid participantId, int? pageSize, int? pageNumber)
        {
            var result = await _scoreRepository.GetAllAsync(null, null);

            if (!result.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<ScoreDTO>>>.Failure(result.Error!);
            }

            //var entities = result.Value.Item2.Select(m => _mapper.Map<TDto>(m));

            var scores = result.Value.Item2.Where(i => i.ParticipantId == participantId);
            //var result = scores.Item2.Where(i => i.ParticipantId == participantId);


            var scoreDTOS = scores.Select(m => _mapper.Map<ScoreDTO>(m)).ToList();

            return ResultT<Tuple<int, IEnumerable<ScoreDTO>>>.Success(new Tuple<int, IEnumerable<ScoreDTO>>(result.Value.Item1, scoreDTOS));

            //return ResultT<Tuple<int, IEnumerable<TDto>>>.Success(new Tuple<int, IEnumerable<TDto>>(result.Value.Item1, entities));

        }
    }
}

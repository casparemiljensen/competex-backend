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

        public async Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> GetMatchesByRoundIdAsync(Guid id, int? pageSize, int? pageNumber)
        {
            var result = await _matchRepository.GetMatchesByRoundId(id, pageSize, pageNumber);
           
            if (result.IsSuccess && result.Value != null)
            {
                return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Success(new Tuple<int, IEnumerable<MatchDTO>>(result.Value.Item1, result.Value.Item2.Select(match => _mapper.Map<MatchDTO>(match))));
            }
            return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Failure(result.Error ?? Error.Failure("UnknownError", "An unknown error occurred."));
        }
    }
}

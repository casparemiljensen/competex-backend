using System.Xml.Schema;
using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.PostgressDataAccess;
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

        new public async Task<ResultT<Tuple<int, IEnumerable<MatchDTO>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var result = await _matchRepository.GetAllAsync(pageSize, pageNumber);
            var ids = result.Value.Item2.Select(m => m.Id).ToList();
            var participantIds = await PostgresConnection.GetGuidsBatch(ids, "MatchParticipants", "MatchId", "ParticipantId");
            var entities = result.Value.Item2.Select(m => _mapper.Map<MatchDTO>(m)).ToList();
            var entitiesResult = entities.Select((m, index) => new { m, participantIds = participantIds[index] });
            return ResultT<Tuple<int, IEnumerable<MatchDTO>>>.Success(new Tuple<int, IEnumerable<MatchDTO>>(result.Value.Item1, entities));
        }
    }
}

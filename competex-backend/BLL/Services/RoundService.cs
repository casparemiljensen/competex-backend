using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class RoundService : GenericService<Round, RoundDTO>, IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public RoundService(IGenericRepository<Round> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _roundRepository = (IRoundRepository)repository;
            _mapper = mapper;
        }

        public async Task<ResultT<IEnumerable<RoundDTO>>> GetByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber)
        {
            var result = await _roundRepository.GetRoundIdsByCompetitionId(competitionId, pageSize, pageNumber);
            if (result.IsSuccess && result.Value != null)
            {
                return ResultT<IEnumerable<RoundDTO>>.Success(result.Value.Select(round => _mapper.Map<RoundDTO>(round)));
            }
            return ResultT<IEnumerable<RoundDTO>>.Failure(result.Error ?? Error.Failure("UnknownError", "An unknown error occurred."));
        }
    }
}

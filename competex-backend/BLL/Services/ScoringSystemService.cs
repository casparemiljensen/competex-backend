using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ScoringSystemService : GenericService<ScoringSystem, ScoringSystemDTO>, IScoringSystemService
    {
        private readonly IScoringSystemRepository _scoringSystemRepository;
        private readonly IMapper _mapper;

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public ScoringSystemService(IGenericRepository<ScoringSystem> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _scoringSystemRepository = (IScoringSystemRepository)repository;
            _mapper = mapper;
        }
    }
}

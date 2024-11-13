using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class CompetitionTypeService : GenericService<CompetitionType, CompetitionTypeDTO>, ICompetitionTypeService
    {
        private readonly ICompetitionTypeRepository _competitionTypeRepository;
        private readonly IMapper _mapper;

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public CompetitionTypeService(IGenericRepository<CompetitionType> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _competitionTypeRepository = (ICompetitionTypeRepository)repository;
            _mapper = mapper;
        }
    }
}

using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class SportTypeService : GenericService<SportType, SportTypeDTO>, ISportTypeService
    {
        private readonly ISportTypeRepository _sportTypeRepository;
        private readonly IMapper _mapper;

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public SportTypeService(IGenericRepository<SportType> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _sportTypeRepository = (ISportTypeRepository)repository;
            _mapper = mapper;
        }
    }
}

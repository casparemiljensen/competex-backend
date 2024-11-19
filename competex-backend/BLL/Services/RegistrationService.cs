using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class RegistrationService : GenericService<Registration, RegistrationDTO>, IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public RegistrationService(IGenericRepository<Registration> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _registrationRepository = (IRegistrationRepository)repository;
            _mapper = mapper;
        }
    }
}

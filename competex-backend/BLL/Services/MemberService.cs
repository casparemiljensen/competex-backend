using AutoMapper;
using AutoMapper.Execution;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Member = competex_backend.Models.Member;

namespace competex_backend.BLL.Services
{
    public class MemberService : GenericService<Member, MemberDTO>, IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;


        public MemberService(IGenericRepository<Member> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _memberRepository = (IMemberRepository)repository;
            _mapper = mapper;
        }

    }
}

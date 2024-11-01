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

        //public MemberService(IMemberRepository memberRepository, IMapper mapper)
        //{
        //    _memberRepository = memberRepository;
        //    _mapper = mapper;
        //}

        public MemberService(IGenericRepository<Member> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _memberRepository = (IMemberRepository)repository;
            _mapper = mapper;
        }

        public bool CheckNumber()
        {
            return true;
        }

        public MemberDTO GetByName(string firstName)
        {
            var member = _memberRepository.GetByFirstNameAsync(firstName).Result;
            if (member == null)
                return null;
            return _mapper.Map<MemberDTO>(member);
        }

        //public bool Create(MemberDTO obj)
        //{
        //    // Map MemberDto to Member
        //    var member = _mapper.Map<Member>(obj);
        //    _memberRepository.InsertAsync(member);
        //}
        //}

        //public bool Update(MemberDTO obj)
        //{
        //    var member = _mapper.Map<Member>(obj);
        //    _memberRepository.UpdateAsync(member);
        //}
        //}

        //public bool Remove(Guid id)
        //{
        //    _memberRepository.DeleteAsync(id);
        //}
    }
}

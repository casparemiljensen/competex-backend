using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public MemberDTO? GetById(Guid id)
        {
            var member = _memberRepository.GetById(id);
            if (member == null)
                return null;
            return _mapper.Map<MemberDTO>(member);
        }

        public IEnumerable<MemberDTO> GetAll()
        {
            var members = _memberRepository.GetAll();
            // Map Member to MemberDto
            var memberDtos = new List<MemberDTO>();
            foreach (var member in members)
            {
                memberDtos.Add(_mapper.Map<MemberDTO>(member));
            }
            return memberDtos; // Return the list of DTOs
        }

        public bool Create(MemberDTO obj)
        {
            // Map MemberDto to Member
            var member = _mapper.Map<Member>(obj);
            _memberRepository.Insert(member);
            return true;
        }

        public bool Update(MemberDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

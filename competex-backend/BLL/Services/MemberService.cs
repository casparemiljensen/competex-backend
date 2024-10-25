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

        public bool AddMember(MemberDTO memberDto)
        {
            // Map MemberDto to Member
            var member = _mapper.Map<Member>(memberDto);
            _memberRepository.AddMember(member);
            return true;
        }

        public IEnumerable<MemberDTO> GetMembers()
        {
            var members = _memberRepository.GetMembers();
            // Map Member to MemberDto
            var memberDtos = new List<MemberDTO>();
            foreach (var member in members)
            {
                memberDtos.Add(_mapper.Map<MemberDTO>(member));
            }
            return memberDtos; // Return the list of DTOs
        }

        
    }
}

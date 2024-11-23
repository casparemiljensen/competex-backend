using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;

namespace competex_backend.Common.Helpers
{
    public class TeamMembersResolver : IValueResolver<Team, TeamDetailDTO, List<MemberDTO>>
    {
        private readonly IMemberService _memberService;

        public TeamMembersResolver(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public List<MemberDTO> Resolve(Team source, TeamDetailDTO destination, List<MemberDTO> destMember, ResolutionContext context)
        {
            var memberDtos = new List<MemberDTO>();

            foreach (var id in source.MemberIds)
            {
                var result = _memberService.GetByIdAsync(id).GetAwaiter().GetResult();
                if (result != null && result.IsSuccess) // Check IsSuccess before accessing Value
                {
                    memberDtos.Add(result.Value);
                }
            }

            return memberDtos;
        }
    }
}

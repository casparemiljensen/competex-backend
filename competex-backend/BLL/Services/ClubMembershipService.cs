using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace competex_backend.BLL.Services
{
    public class ClubMembershipService : GenericService<ClubMembership, ClubMembershipDTO>, IClubMembershipService
    {
        private readonly IClubMembershipRepository _clubMembershipRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        public ClubMembershipService(IGenericRepository<ClubMembership> repository, IMapper mapper, IMemberRepository memberRepository) : base(repository, mapper)
        {
            _clubMembershipRepository = (IClubMembershipRepository)repository;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public async Task<ResultT<Tuple<int, IEnumerable<ClubDTO>>>> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber)
        {
            // Call the repository method to retrieve clubs for a given memberId
            var clubsResult = await _clubMembershipRepository.GetClubsOfMemberAsync(memberId, pageSize, pageNumber);
            if (!clubsResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<ClubDTO>>>.Failure(clubsResult.Error!);
            }
            var entities = clubsResult.Value.Item2.Select(m => _mapper.Map<ClubDTO>(m));
            return ResultT<Tuple<int, IEnumerable<ClubDTO>>>.Success(new Tuple<int, IEnumerable<ClubDTO>>(clubsResult.Value.Item1, entities));
        }

        public async Task<ResultT<Tuple<int, IEnumerable<MemberDTO>>>> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber)
        {
            // Directly call the repository method to retrieve members for a given clubId
            var membersResult = await _clubMembershipRepository.GetMembersOfClubAsync(clubId, pageSize, pageNumber);
            if (!membersResult.IsSuccess)
            {
                return ResultT<Tuple<int, IEnumerable<MemberDTO>>>.Failure(membersResult.Error!);
            }
            var entities = membersResult.Value.Item2.Select(m => _mapper.Map<MemberDTO>(m));
            return ResultT<Tuple<int, IEnumerable<MemberDTO>>>.Success(new Tuple<int, IEnumerable<MemberDTO>>(membersResult.Value.Item1, entities));
        }
    }
}

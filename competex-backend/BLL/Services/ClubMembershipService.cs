using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ClubMembershipService : GenericService<ClubMembership, ClubMembershipDTO>, IClubMembershipService
    {
        private readonly IClubMembershipRepository _clubMembershipRepository;
        private readonly IMapper _mapper;
        public ClubMembershipService(IGenericRepository<ClubMembership> repository, IMapper mapper) : base(repository, mapper)
        {
            _clubMembershipRepository = (IClubMembershipRepository)repository;
            _mapper = mapper;
        }

        public Task<Result> CreateEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultT<List<Club>>> GetClubsOfMemberAsync(Guid memberId)
        {
            // Call the repository method to retrieve clubs for a given memberId
            var clubsResult = await _clubMembershipRepository.GetClubsOfMemberAsync(memberId);
            return clubsResult;
        }

        public async Task<ResultT<List<Member>>> GetMembersOfClubAsync(Guid clubId)
        {
            // Directly call the repository method to retrieve members for a given clubId
            var membersResult = await _clubMembershipRepository.GetMembersOfClubAsync(clubId);
            return membersResult;
        }
    }
}

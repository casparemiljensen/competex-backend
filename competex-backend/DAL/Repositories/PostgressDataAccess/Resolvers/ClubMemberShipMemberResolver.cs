using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgressDataAccess.Resolvers
{
    public class ClubMemberShipMemberResolver
    {
        private static IClubMembershipRepository _clubMembershipRepository;
        private static IMemberRepository _memberRepository;

        public ClubMemberShipMemberResolver(IClubMembershipRepository clubMembershipRepository, IMemberRepository memberRepository)
        {
            _clubMembershipRepository = clubMembershipRepository;
            _memberRepository = memberRepository;
        }

        public static Task<ResultT<Tuple<int, IEnumerable<Member>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            return _memberRepository.SearchAllAsync(pageSize, pageNumber, filters);
        }
    }
}

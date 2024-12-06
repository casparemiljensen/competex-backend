using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess.Resolvers
{
    public class MatchScoresResolver
    {
        private static IMatchRepository _matchRepository;
        private static IScoreRepository _scoreRepository;

        public MatchScoresResolver(IMatchRepository matchRepository, IScoreRepository scoreRepository)
        {
            _matchRepository = matchRepository;
            _scoreRepository = scoreRepository;
        }

        public static async Task<Result> DeleteByPropertyId(string propertyName, Guid id, string? tableName = null, string? nextProperty = null)
        {
            return await _scoreRepository.DeleteByPropertyId(propertyName, id, tableName, nextProperty);
        }
    }
}

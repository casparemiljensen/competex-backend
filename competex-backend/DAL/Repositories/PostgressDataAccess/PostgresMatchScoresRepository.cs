using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using competex_backend.Models.ManyMany;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class PostgresMatchScoresRepository : PostgresGenericRepository<MatchScores>, IMatchScoresRepository
    {
        private static IScoreRepository _scoreRepository;
        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            return await base.DeleteAsync(id, skipRecursion);
        }

        public static async Task<Result> DeleteByPropertyId(string propertyName, Guid id, string? tableName = null, string? nextProperty = null)
        {
            return await _scoreRepository.DeleteByPropertyId(propertyName, id, tableName, nextProperty);
        }
    }
}

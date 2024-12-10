using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using competex_backend.Models.ManyMany;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    public class PostgresMatchScoresRepository : PostgresGenericRepository<MatchScores>, IMatchScoresRepository
    {
        private IScoreRepository _scoreRepository;
        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            if (propertyName == null)
            {
                throw new NullReferenceException("propertyName cannot be null in matchScore");
            }
            Console.WriteLine(propertyName + ": " + id.ToString());
            return await base.DeleteByPropertyId(propertyName, id, null, "MatchId");
        }

        new public async Task<Result> DeleteByPropertyId(string propertyName, Guid id, string? tableName = null, string? nextProperty = null)
        {
            return await base.DeleteByPropertyId("MatchId", id);
            await _scoreRepository.DeleteByPropertyId(propertyName, id, tableName, nextProperty);
        }
    }
}

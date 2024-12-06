using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresScoreRepository : PostgresGenericRepository<Score>, IScoreRepository
    {
        private static PostgresGenericRepository<Score> _postgresGenericRepository = new PostgresGenericRepository<Score>();

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);
            var result = await base.DeleteByPropertyId("ScoreId", id, "MatchScores", "ScoreId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteByPropertyId("ScoreId", id, "ScorePenalties", "PenaltyId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

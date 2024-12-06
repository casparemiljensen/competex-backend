using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;
using competex_backend.DAL.Repositories.PostgressDataAccess.Resolvers;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMatchRepository : PostgresGenericRepository<Match>, IMatchRepository
    {
        private static PostgresGenericRepository<Match> _postgresGenericRepository = new PostgresGenericRepository<Match>();


        public Task<Match?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            var result = await MatchScoresResolver.DeleteByPropertyId("ParticipantId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }
            Console.WriteLine("Match:" + skipRecursion);
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);

            result = await base.DeleteByPropertyId("MatchId", id, "MatchParticipants", "ParticipantId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteByPropertyId("MatchId", id, "MatchScores", "ScoreId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteByPropertyId("MatchId", id, "RoundMatches", "RoundId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

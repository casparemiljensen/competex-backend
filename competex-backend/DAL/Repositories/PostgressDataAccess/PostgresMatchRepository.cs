using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMatchRepository : PostgresGenericRepository<Match>, IMatchRepository
    {
        private static PostgresGenericRepository<Match> _postgresGenericRepository = new PostgresGenericRepository<Match>();

        public Task<Match?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id)
        {
            var result = await base.DeleteFromTable("MatchParticipants", "MatchId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteFromTable("MatchScores", "MatchId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id);
        }
    }
}

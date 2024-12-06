using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresParticipantRepository : PostgresGenericRepository<Participant>, IParticipantRepository
    {
        private static PostgresGenericRepository<Participant> _postgresGenericRepository = new PostgresGenericRepository<Participant>();

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            var result = await base.DeleteFromTable("MatchParticipants", "ParticipantId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteFromTable("ParticipantMembers", "ParticipantId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

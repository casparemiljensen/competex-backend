using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresPenaltyRepository : PostgresGenericRepository<Penalty>, IPenaltyRepository
    {
        private static PostgresGenericRepository<Penalty> _postgresGenericRepository = new PostgresGenericRepository<Penalty>();

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);
            var result = await base.DeleteByPropertyId("PenaltyId", id, "ScorePenalties", "PenaltyId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);    
        }
    }
}

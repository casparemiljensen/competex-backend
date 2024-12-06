using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresAdminRepository : PostgresGenericRepository<Admin>, IAdminRepository
    {
        private static PostgresGenericRepository<Admin> _postgresGenericRepository = new PostgresGenericRepository<Admin>();
        

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);
            var result = await base.DeleteByPropertyId("AdminId", id, "AdminSportType", "SportTypeId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

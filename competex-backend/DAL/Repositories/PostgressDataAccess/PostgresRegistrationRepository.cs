using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresRegistrationRepository : PostgresGenericRepository<Registration>, IRegistrationRepository
    {
        private static PostgresGenericRepository<Registration> _postgresGenericRepository = new PostgresGenericRepository<Registration>();

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }
    }
}

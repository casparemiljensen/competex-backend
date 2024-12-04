using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresSportTypeRepository : PostgresGenericRepository<SportType>, ISportTypeRepository
    {
        private static PostgresGenericRepository<SportType> _postgresGenericRepository = new PostgresGenericRepository<SportType>();

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }
    }
}

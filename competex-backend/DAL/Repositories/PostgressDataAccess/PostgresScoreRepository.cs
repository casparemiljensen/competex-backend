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

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }
    }
}

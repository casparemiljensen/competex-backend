using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresFieldRepository : PostgresGenericRepository<Field>, IFieldRepository
    {
        private static PostgresGenericRepository<Field> _postgresGenericRepository = new PostgresGenericRepository<Field>();
    }
}

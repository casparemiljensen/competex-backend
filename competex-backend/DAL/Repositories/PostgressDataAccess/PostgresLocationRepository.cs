using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresLocationRepository : PostgresGenericRepository<Location>, ILocationRepository
    {
        private static PostgresGenericRepository<Location> _postgresGenericRepository = new PostgresGenericRepository<Location>();
    }
}

using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresEventRepository : PostgresGenericRepository<Event>, IEventRepository
    {
        private static PostgresGenericRepository<Event> _postgresGenericRepository = new PostgresGenericRepository<Event>();

        public Task<Result> AddCompetition(Guid eventId, Competition competition)
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }
    }
}

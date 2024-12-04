using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresRoundRepository : PostgresGenericRepository<Round>, IRoundRepository
    {
        private static PostgresGenericRepository<Round> _postgresGenericRepository = new PostgresGenericRepository<Round>();

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }

        public Task<ResultT<Tuple<int, IEnumerable<Round>>>> GetRoundIdsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}

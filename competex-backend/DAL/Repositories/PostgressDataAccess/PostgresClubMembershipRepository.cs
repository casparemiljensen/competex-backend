using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresClubMembershipRepository : PostgresGenericRepository<ClubMembership>, IClubMembershipRepository
    {
        private static PostgresGenericRepository<ClubMembership> _postgresGenericRepository = new PostgresGenericRepository<ClubMembership>();

        public Task<Result> CreateEventAsync()
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);    
        }

        public Task<ResultT<Tuple<int, IEnumerable<Club>>>> GetClubsOfMemberAsync(Guid memberId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<Tuple<int, IEnumerable<Member>>>> GetMembersOfClubAsync(Guid clubId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}

using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : PostgresGenericRepository<Member>, IMemberRepository
    {
        private static PostgresGenericRepository<Member> _postgresGenericRepository = new PostgresGenericRepository<Member>();

        public Task<Member?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id)
        {
            var result = await base.DeleteFromTable("Judge", "MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteFromTable("ParticipantMembers", "MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id);    
        }
    }
}

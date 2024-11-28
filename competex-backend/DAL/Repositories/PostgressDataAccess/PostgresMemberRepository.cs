using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : PostgresGenericRepository<Member>, IMemberRepository
    {
        private static PostgresGenericRepository<Member> _postgresGenericRepository = new PostgresGenericRepository<Member>();

        public Task<Member?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }
    }
}

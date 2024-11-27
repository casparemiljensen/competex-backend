using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        public Task<Result> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultT<Tuple<int, IEnumerable<Member>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            var members = await PostgresConnection.GetAll<Member>();
            return new Tuple<int, IEnumerable<Member>>(PaginationHelper.GetTotalPages(pageSize, pageNumber, members.Count()), members); 
        }

        public Task<Member?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<Member>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<Guid>> InsertAsync(Member obj)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<Tuple<int, IEnumerable<Member>>>> SearchAllAsync(int? pageSize, int? pageNumber, Dictionary<string, object>? filters)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAsync(Guid id, Member obj)
        {
            throw new NotImplementedException();
        }
    }
}

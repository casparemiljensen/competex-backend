using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresClubRepository : PostgresGenericRepository<Club>, IClubRepository
    {
        private static PostgresGenericRepository<Club> _postgresGenericRepository = new PostgresGenericRepository<Club>();
        private IRoundRepository _roundRepository;
        private IClubMembershipRepository _clubMembershipRepository;
        
        public PostgresClubRepository(IRoundRepository roundRepository, IClubMembershipRepository clubMembershipRepository)
        {
            _roundRepository = roundRepository;
            _clubMembershipRepository = clubMembershipRepository;
        }
        

        public Task<ResultT<IEnumerable<Club>>> GetClubsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            await _roundRepository.DeleteByPropertyId("CompetitionId", id);
            await _clubMembershipRepository.DeleteByPropertyId("ClubId", id);

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

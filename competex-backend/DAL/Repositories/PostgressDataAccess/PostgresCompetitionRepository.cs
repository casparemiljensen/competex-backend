using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresCompetitionRepository : PostgresGenericRepository<Competition>, ICompetitionRepository
    {
        private static PostgresGenericRepository<Competition> _postgresGenericRepository = new PostgresGenericRepository<Competition>();
        private IRegistrationRepository _registrationRepository;
        private IRoundRepository _roundRepository;
        public PostgresCompetitionRepository(IRegistrationRepository registrationRepository, IRoundRepository roundRepository)
        {
            _registrationRepository = registrationRepository;
            _roundRepository = roundRepository;
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            
            
            var result = await _registrationRepository.DeleteByPropertyId("CompetitionId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _roundRepository.DeleteByPropertyId("CompetitionId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

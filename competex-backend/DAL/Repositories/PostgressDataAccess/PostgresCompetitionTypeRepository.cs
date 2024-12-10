using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresCompetitionTypeRepository : PostgresGenericRepository<CompetitionType>, ICompetitionTypeRepository
    {
        private static PostgresGenericRepository<CompetitionType> _postgresGenericRepository = new PostgresGenericRepository<CompetitionType>();
        private ICompetitionRepository _competitionRepository;

        public PostgresCompetitionTypeRepository(ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            var result = await _competitionRepository.DeleteByPropertyId("CompetitionType", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);

            result = await base.DeleteFromTable("data_CompetitionType_CompetitionAttributes", "CompetitionTypeId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

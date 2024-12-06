using competex_backend.Models;
using competex_backend.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Common.Helpers;
using Npgsql;
using NpgsqlTypes;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresSportTypeRepository : PostgresGenericRepository<SportType>, ISportTypeRepository
    {
        private static PostgresGenericRepository<SportType> _postgresGenericRepository = new PostgresGenericRepository<SportType>();
        private IEventRepository _eventRepository;

        public PostgresSportTypeRepository(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            var result = await base.DeleteFromTable("AdminSportType", "SportTypeId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _eventRepository.DeleteByPropertyId("SportType", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }
    }
}

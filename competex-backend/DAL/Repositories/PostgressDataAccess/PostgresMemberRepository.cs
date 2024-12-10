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
        private IJudgeRepository _judgeRepository;
        private IClubMembershipRepository _clubMembershipRepository;
        private IEntityRepository _entityRepository;
        private IEventRepository _eventRepository;
        private IRegistrationRepository _registrationRepository;

        public PostgresMemberRepository(IJudgeRepository judgeRepository, IClubMembershipRepository clubMembershipRepository, IEntityRepository entityRepository, IEventRepository eventRepository, IRegistrationRepository registrationRepository)
        {
            _judgeRepository = judgeRepository;
            _clubMembershipRepository = clubMembershipRepository;
            _entityRepository = entityRepository;
            _eventRepository = eventRepository;
            _registrationRepository = registrationRepository;
        }

        public Task<Member?> GetByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion, string? propertyName = null)
        {
            var result = await _clubMembershipRepository.DeleteByPropertyId("MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _judgeRepository.DeleteByPropertyId("MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteFromTable("ParticipantMembers", "MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _entityRepository.DeleteByPropertyId("Owner", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _judgeRepository.DeleteByPropertyId("Organizer", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteFromTable("ParticipantMembers", "MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await _registrationRepository.DeleteByPropertyId("MemberId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);    
        }
    }
}

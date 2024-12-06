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
        private IMatchRepository _matchRepository;

        public PostgresRoundRepository(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async override Task<Result> DeleteAsync(Guid id, bool skipRecursion)
        {
            var result = await _matchRepository.DeleteByPropertyId("RoundId", id);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }
            if (skipRecursion) return await base.DeleteAsync(id, skipRecursion);

            result = await base.DeleteByPropertyId("RoundId", id, "RoundParticipants", "ParticipantId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            result = await base.DeleteByPropertyId("RoundId", id, "RoundMatches", "MatchId");
            if (!result.IsSuccess)
            {
                return result.Error!;
            }

            return await base.DeleteAsync(id, skipRecursion);
        }

        public Task<ResultT<Tuple<int, IEnumerable<Round>>>> GetRoundIdsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}

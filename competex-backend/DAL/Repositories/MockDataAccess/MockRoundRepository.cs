using System;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockRoundRepository : MockGenericRepository<Round>, IRoundRepository
{
    public MockRoundRepository(MockDatabaseManager db) : base(db)
    {
    }
    public async Task<ResultT<IEnumerable<Guid>>> GetRoundIdsByCompetitionId(Guid competitionId)
    {
        var guids = await Task.Run(() => _entities.FindAll(round => round.CompetitionId == competitionId)
            .Select(round => round.Id));
        return ResultT<IEnumerable<Guid>>.Success(guids);
    }
}

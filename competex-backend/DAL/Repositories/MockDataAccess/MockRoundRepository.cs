using System;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockRoundRepository : MockGenericRepository<Round>, IRoundRepository
{
    public MockRoundRepository(MockDatabaseManager db) : base(db)
    {
    }
    public IEnumerable<Guid> GetRoundIdsByCompetitionId(Guid competitionId)
    {
        return _entities.FindAll(round => round.CompetitionId == competitionId).Select(round => round.Id);
    }
}

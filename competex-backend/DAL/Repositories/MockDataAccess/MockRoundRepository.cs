using System;
using competex_backend.API.DTOs;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockRoundRepository : MockGenericRepository<Round>, IRoundRepository
{
    public MockRoundRepository(MockDatabaseManager db) : base(db)
    {
    }
    public async Task<ResultT<IEnumerable<Round>>> GetRoundIdsByCompetitionId(Guid competitionId)
    {
        var rounds = await Task.Run(() => _entities.FindAll(round => round.CompetitionId == competitionId));
        return ResultT<IEnumerable<Round>>.Success(rounds);
    }
}

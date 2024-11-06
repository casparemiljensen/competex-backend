using System;
using competex_backend.API.DTOs;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockRoundRepository : MockGenericRepository<Round>, IRoundRepository
{
    public MockRoundRepository(MockDatabaseManager db) : base(db)
    {
    }
    public async Task<ResultT<Tuple<int, IEnumerable<Round>>>> GetRoundIdsByCompetitionId(Guid competitionId, int? pageSize, int? pageNumber)
    {
        var entities = await Task.Run(() => _entities.FindAll(round => round.CompetitionId == competitionId));
        var rounds = await Task.Run(() => entities
        .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
        .Take(pageSize ?? Defaults.PageSize)); //TODO: Make defaults
        return ResultT<Tuple<int, IEnumerable<Round>>>.Success(
            new Tuple<int, IEnumerable<Round>>(
                PaginationHelper.GetTotalPages(pageSize, pageNumber, entities.Count),
                rounds));
    }
}

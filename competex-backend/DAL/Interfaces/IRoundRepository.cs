using System;
using competex_backend.API.DTOs;
using competex_backend.Models;

namespace competex_backend.DAL.Interfaces;

public interface IRoundRepository : IGenericRepository<Round>
{
    public Task<ResultT<IEnumerable<Round>>> GetRoundIdsByCompetitionId(Guid CompetitionId, int? pageSize, int? pageNumber);
}

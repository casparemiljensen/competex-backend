using System;
using competex_backend.Models;

namespace competex_backend.DAL.Interfaces;

public interface IRoundRepository : IGenericRepository<Round>
{
    public Task<ResultT<IEnumerable<Guid>>> GetRoundIdsByCompetitionId(Guid CompetitionId);
}

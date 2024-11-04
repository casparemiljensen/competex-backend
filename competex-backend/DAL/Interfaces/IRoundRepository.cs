using System;
using competex_backend.Models;

namespace competex_backend.DAL.Interfaces;

public interface IRoundRepository : IGenericRepository<Round>
{
    public IEnumerable<Guid> GetRoundIdsByCompetitionId(Guid CompetitionId);
}

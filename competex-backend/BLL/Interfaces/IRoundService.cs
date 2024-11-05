﻿using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IRoundService : IGenericService<RoundDTO>
    {
        Task<ResultT<IEnumerable<RoundDTO>>> GetByCompetitionId(Guid competitionId);
    }
}

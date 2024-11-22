using competex_backend.API.DTOs;

namespace competex_backend.BLL.Interfaces
{
    public interface IEventService : IGenericService<EventDTO>
    {
        Task<Result> AddCompetition(Guid eventId, Guid competitionId);
        public Task<ResultT<int>> GetMembersOwedAmount(Guid memberId, Guid eventId);
    }
}

using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Result> AddCompetition(Guid eventId, Competition competition);
    }
}

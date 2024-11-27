using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{
    public class MockEventRepository : MockGenericRepository<Event>, IEventRepository
    {
        public MockEventRepository(MockDatabaseManager db) : base(db)
        {
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously - Will need to be later
        public async Task<Result> AddCompetition(Guid eventId, Competition competition)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var _event = _entities.Find(_event => _event.Id == eventId);

            if (_event == null)
            {
                return Result.Failure(Error.NotFound("NotFound", $"Could not find event with Id: {eventId}"));
            }

            _event.AddCompetition(competition);
            return Result.Success();
        }
    }
}
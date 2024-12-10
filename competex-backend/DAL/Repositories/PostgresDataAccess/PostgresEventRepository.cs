using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresEventRepository : PostgresGenericRepository<Event>, IEventRepository
    {
        public PostgresEventRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<Result> AddCompetition(Guid eventId, Guid competitionId) // TODO: Implement this method
        {
            throw new NotImplementedException();
        }

        public override async Task<ResultT<Event>> GetByIdAsync(Guid id)
        {
            // Retrieve the entity by ID
            var entity = await _dbContext.Events.FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null)
            {
                return ResultT<Event>.Failure(Error.NotFound($"{typeof(Event).Name.ToLower()} not found.", $"{typeof(Event).Name.ToLower()} with ID {id} does not exist."));
            }

            // Check if the entity is an Event and populate CompetitionIds
            if (entity is Event @event)
            {
                var competitionIds = await _dbContext.Set<Dictionary<string, object>>("event_competitions")
                    .Where(ec => EF.Property<Guid>(ec, "EventId") == id)
                    .Select(ec => EF.Property<Guid>(ec, "CompetitionId"))
                    .ToListAsync();

                @event.CompetitionIds = competitionIds;
            }

            return ResultT<Event>.Success(entity);
        }

        public override async Task<ResultT<Tuple<int, IEnumerable<Event>>>> GetAllAsync(int? pageSize, int? pageNumber)
        {
            // Retrieve the queryable set
            var query = _dbSet.AsNoTracking();

            // Calculate total pages
            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, await query.CountAsync());

            // Paginate and fetch results
            var entities = await query
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize)
                .ToListAsync();

            // Populate CompetitionIds for Event entities
            foreach (var entity in entities)
            {
                if (entity is Event @event)
                {
                    var competitionIds = await _dbContext.Set<Dictionary<string, object>>("event_competitions")
                        .Where(ec => EF.Property<Guid>(ec, "EventId") == @event.Id)
                        .Select(ec => EF.Property<Guid>(ec, "CompetitionId"))
                        .ToListAsync();

                    @event.CompetitionIds = competitionIds;
                }
            }

            return ResultT<Tuple<int, IEnumerable<Event>>>.Success(new Tuple<int, IEnumerable<Event>>(totalPages, entities));
        }


    }
}



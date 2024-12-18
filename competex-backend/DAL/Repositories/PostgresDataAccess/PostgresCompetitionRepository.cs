using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresCompetitionRepository : PostgresGenericRepository<Competition>, ICompetitionRepository
    {
        public PostgresCompetitionRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        //public override async Task<ResultT<Guid>> InsertAsync(Competition entity)
        //{
        //    if (entity == null)
        //        return ResultT<Guid>.Failure(Error.Validation("404", "Event entity cannot be null."));

        //    try
        //    {
        //        // Insert the main Event entity
        //        await _dbSet.AddAsync(entity);
        //        await _dbContext.SaveChangesAsync();

        //        // Insert into the event_competitions join table
        //        if (entity.CompetitionIds != null && entity.CompetitionIds.Any())
        //        {
        //            var eventCompetitions = entity.CompetitionIds.Select(competitionId => new Dictionary<string, object>
        //    {
        //        { "EventId", entity.Id },
        //        { "CompetitionId", competitionId }
        //    }).ToList();

        //            foreach (var eventCompetition in eventCompetitions)
        //            {
        //                await _dbContext.Set<Dictionary<string, object>>("event_competitions").AddAsync(eventCompetition);
        //            }

        //            await _dbContext.SaveChangesAsync();
        //        }

        //        return ResultT<Guid>.Success(entity.Id);

        //    }
        //    catch (Exception ex)
        //    {
        //        return ResultT<Guid>.Failure(Error.Failure("500", $"Failed to insert event: {ex.Message}"));
        //    }
        //}

        public override async Task<ResultT<Guid>> InsertAsync(Competition entity)
        {
            if (entity == null)
                return ResultT<Guid>.Failure(Error.Validation("404", "Competition entity cannot be null."));

            try
            {
                // Validate the EventId
                if (entity.EventId == Guid.Empty)
                    return ResultT<Guid>.Failure(Error.Validation("400", "EventId is required for a Competition."));

                // Insert the Competition entity
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                // Insert into the event_competitions join table
                var eventCompetition = new Dictionary<string, object>
                    {
                        { "EventId", (Guid)entity.EventId },
                        { "CompetitionId", (Guid)entity.Id }
                    };

                await _dbContext.Set<Dictionary<string, object>>("event_competitions").AddAsync(eventCompetition);
                await _dbContext.SaveChangesAsync();

                return ResultT<Guid>.Success(entity.Id);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("500", $"Failed to insert competition: {ex.Message}"));
            }
        }

        public override async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                // Retrieve the Competition entity to delete
                var entityToRemove = await _dbSet.FindAsync(id);
                if (entityToRemove == null)
                {
                    return Result.Failure(Error.NotFound("NotFound", $"Competition with ID {id} does not exist."));
                }

                // Remove the related entry in the event_competitions join table
                var eventCompetitionsSet = _dbContext.Set<Dictionary<string, object>>("event_competitions");
                var eventCompetitionToRemove = await eventCompetitionsSet.FirstOrDefaultAsync(ec =>
                    (Guid)ec["CompetitionId"] == id);

                if (eventCompetitionToRemove != null)
                {
                    eventCompetitionsSet.Remove(eventCompetitionToRemove);
                }

                // Remove the Competition entity
                _dbSet.Remove(entityToRemove);
                await _dbContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Failed to delete competition: {ex.Message}"));
            }
        }
    }
}

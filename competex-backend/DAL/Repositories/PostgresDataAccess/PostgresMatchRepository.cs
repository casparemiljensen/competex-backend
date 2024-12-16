using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresMatchRepository : PostgresGenericRepository<Match>, IMatchRepository
    {
        public PostgresMatchRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override async Task<ResultT<Match>> GetByIdAsync(Guid id)
        {
            // Retrieve the entity by ID
            var entity = await _dbContext.Matches.FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null)
            {
                return ResultT<Match>.Failure(Error.NotFound($"{typeof(Match).Name.ToLower()} not found.", $"{typeof(Match).Name.ToLower()} with ID {id} does not exist."));
            }

            // Check if the entity is an Event and populate CompetitionIds
            if (entity is Match @match)
            {
                var participantIds = await _dbContext.Set<Dictionary<string, object>>("match_participants")
                    .Where(ec => EF.Property<Guid>(ec, "MatchId") == id)
                    .Select(ec => EF.Property<Guid>(ec, "ParticipantId"))
                    .ToListAsync();

                @match.ParticipantIds = participantIds;
            }

            return ResultT<Match>.Success(entity);
        }

        public override async Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetAllAsync(int? pageSize, int? pageNumber)
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
                if (entity is Match @match)
                {
                    var participantIds = await _dbContext.Set<Dictionary<string, object>>("match_participants")
                        .Where(ec => EF.Property<Guid>(ec, "MatchId") == match.Id)
                        .Select(ec => EF.Property<Guid>(ec, "ParticipantId"))
                        .ToListAsync();

                    @match.ParticipantIds = participantIds;
                }
            }

            return ResultT<Tuple<int, IEnumerable<Match>>>.Success(new Tuple<int, IEnumerable<Match>>(totalPages, entities));
        }

        public override async Task<ResultT<Guid>> InsertAsync(Match entity)
        {
            if (entity == null)
                return ResultT<Guid>.Failure(Error.Validation("404", "Match entity cannot be null."));

            try
            {
                // Validate the RoundId
                if (entity.RoundId == Guid.Empty)
                    return ResultT<Guid>.Failure(Error.Validation("400", "RoundId is required for a Match."));

                // Insert the Match entity
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                // Insert into the match_participants join table
                if (entity.ParticipantIds != null && entity.ParticipantIds.Any())
                {
                    var matchParticipantsSet = _dbContext.Set<Dictionary<string, object>>("match_participants");

                    foreach (var participantId in entity.ParticipantIds)
                    {
                        var matchParticipant = new Dictionary<string, object>
                        {
                            { "MatchId", entity.Id },
                            { "ParticipantId", participantId }
                        };

                        await matchParticipantsSet.AddAsync(matchParticipant);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return ResultT<Guid>.Success(entity.Id);
            }
            catch (Exception ex)
            {
                return ResultT<Guid>.Failure(Error.Failure("500", $"Failed to insert match: {ex.Message}"));
            }
        }

        public override async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                // Retrieve the Match entity to delete
                var entityToRemove = await _dbSet.FindAsync(id);
                if (entityToRemove == null)
                {
                    return Result.Failure(Error.NotFound("NotFound", $"Match with ID {id} does not exist."));
                }

                // Remove the related entries in the match_participants join table
                var matchParticipantsSet = _dbContext.Set<Dictionary<string, object>>("match_participants");
                var relatedParticipants = matchParticipantsSet.Where(mp => (Guid)mp["MatchId"] == id);

                foreach (var participant in relatedParticipants)
                {
                    matchParticipantsSet.Remove(participant);
                }

                // Remove the Match entity
                _dbSet.Remove(entityToRemove);
                await _dbContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("DeletionError", $"Failed to delete match: {ex.Message}"));
            }
        }

        public async Task<ResultT<Tuple<int, IEnumerable<Match>>>> GetMatchesByRoundId(Guid roundId, int? pageSize, int? pageNumber)
        {
            // Filter matches by RoundId
            var query = _dbContext.Matches.Where(match => match.RoundId == roundId);

            // Count total matches for pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var paginatedMatches = await query
                .Skip(PaginationHelper.GetSkip(pageSize, pageNumber))
                .Take(pageSize ?? Defaults.PageSize)
                .ToListAsync();

            // Calculate total pages
            var totalPages = PaginationHelper.GetTotalPages(pageSize, pageNumber, totalCount);

            // Return result
            return ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                new Tuple<int, IEnumerable<Match>>(totalPages, paginatedMatches));
        }

    }
}



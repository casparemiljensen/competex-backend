using AutoMapper;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Services;
using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;
using Moq;
using Xunit;
using Assert = Xunit.Assert;
using Match = competex_backend.Models.Match;

public class ScoreServiceTests
{
    private readonly Mock<IScoreRepository> _mockScoreRepository;
    private readonly Mock<IRoundRepository> _mockRoundRepository;
    private readonly Mock<IMatchRepository> _mockMatchRepository;
    private readonly Mock<IPenaltyRepository> _mockPenaltyRepository;
    private readonly Mock<IMapper> _mockMapper;

    private readonly ScoreService _scoreService;

    public ScoreServiceTests()
    {
        // Initialize the mocks
        _mockScoreRepository = new Mock<IScoreRepository>();
        _mockRoundRepository = new Mock<IRoundRepository>();
        _mockMatchRepository = new Mock<IMatchRepository>();
        _mockPenaltyRepository = new Mock<IPenaltyRepository>();
        _mockMapper = new Mock<IMapper>();

        // Pass the mocks into the ScoreService constructor
        _scoreService = new ScoreService(
            _mockScoreRepository.Object,
            _mockMapper.Object,
            _mockRoundRepository.Object,
            _mockMatchRepository.Object,
            _mockPenaltyRepository.Object
        );
    }
    
    [Fact]
    public async Task GetResultsByCompetitionId_ReturnsPaginatedResults()
    {
        // Arrange
        var competitionId = Guid.NewGuid();

        var roundIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var rounds = roundIds.Select(id => new Round { Id = id, Name = "RoundOne" }).ToList();

        var matchParticipantIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var matches = new List<Match>
        {
            new Match { ParticipantIds = matchParticipantIds }
        };

        var scores = new List<Score>
        {
            new TimeFaultScore(2, TimeSpan.FromSeconds(120), matches[0].Id, matchParticipantIds[0]),
            new TimeFaultScore(1, TimeSpan.FromSeconds(60), matches[0].Id, matchParticipantIds[0]),
            new TimeFaultScore(2, TimeSpan.FromSeconds(60), matches[0].Id, matchParticipantIds[1]),
        };

        var expectedMappedResults = new List<ScoreResultDTO>
        {
            new ScoreResultDTO { ParticipantId = matchParticipantIds[0], Faults = 15, Time = TimeSpan.FromSeconds(180) },
            new ScoreResultDTO { ParticipantId = matchParticipantIds[1], Faults = 3, Time = TimeSpan.FromSeconds(150) }
        };
        int pageSize = 10;
        int pageNumber = 1;
        _mockRoundRepository
            .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(
                new Tuple<int, IEnumerable<Round>>(1, new List<Round> { new Round { Id = Guid.NewGuid(), Name = "RoundOne" } })
            ));

        _mockMatchRepository
             .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
             .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                 new Tuple<int, IEnumerable<Match>>(2, new List<Match>
                 {
                    new Match { Id = Guid.NewGuid(), ParticipantIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() } }
                 })
             ));

        _mockScoreRepository
            .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Score>>>.Success(
                new Tuple<int, IEnumerable<Score>>(3, new List<Score>
                {
                    new TimeFaultScore(2, TimeSpan.FromSeconds(60), matches[0].Id, matchParticipantIds[1]),
                    new TimeFaultScore(1, TimeSpan.FromSeconds(30), matches[0].Id, matchParticipantIds[1]),
                    new TimeFaultScore(5, TimeSpan.FromSeconds(80), matches[0].Id, matchParticipantIds[0]),
                })
            ));

        _mockMapper
            .Setup(mapper => mapper.Map<ScoreResultDTO>(It.IsAny<ScoreResult>()))
            .Returns((ScoreResult score) => expectedMappedResults.First(x => x.ParticipantId == score.ParticipantId));

        // Act
        var result = await _scoreService.GetResultsByCompetitionId(competitionId, pageSize, pageNumber);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        var paginatedData = result.Value.Values.ToList();
        Assert.Equal(2, paginatedData.Count);

        Assert.Equal(matchParticipantIds[1], paginatedData[0].ParticipantId);
        Assert.Equal(3, paginatedData[0].Faults);
        //Assert.Equal(TimeSpan.FromSeconds(90), paginatedData[0].Time);

        Assert.Equal(matchParticipantIds[0], paginatedData[1].ParticipantId);
        Assert.Equal(15, paginatedData[1].Faults);
        //Assert.Equal(TimeSpan.FromSeconds(150), paginatedData[1].Time);
    }

    [Fact]
    public async Task GetResultsByCompetitionId_NoMatchesFound_ReturnsEmptyPagination()
    {
        // Arrange
        var competitionId = Guid.NewGuid();

        var roundIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var rounds = roundIds.Select(id => new Round { Id = id, Name = "RoundTwo" }).ToList();
        var matchId = Guid.NewGuid();

        _mockRoundRepository
            .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(
                new Tuple<int, IEnumerable<Round>>(1, new List<Round> { new Round { Id = Guid.NewGuid(), Name = "RoundOne" } })
            ));

        _mockScoreRepository
                        .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Score>>>.Success(
                new Tuple<int, IEnumerable<Score>>(0, new List<Score>
                {
                })
            ));

        _mockMatchRepository
             .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
             .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                 new Tuple<int, IEnumerable<Match>>(0, new List<Match>
                 {
                 })
             ));

        var pageSize = 10;
        var pageNumber = 1;

        // Act
        var result = await _scoreService.GetResultsByCompetitionId(competitionId, pageSize, pageNumber);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value.Values);
    }

    [Fact]
    public async Task GetResultsByCompetitionId_HandlesNullParticipantsAndScores()
    {
        // Arrange
        var competitionId = Guid.NewGuid();

        var roundIds = new List<Guid> { Guid.NewGuid() };
        var rounds = roundIds.Select(id => new Round { Id = id, Name = "RoundOne" }).ToList();

        var matches = new List<Match>
        {
            new Match { ParticipantIds = null } // Null participants
        };

        var scores = new List<Score>(); // Empty scores

        _mockRoundRepository
            .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(
                new Tuple<int, IEnumerable<Round>>(1, new List<Round> { new Round { Id = Guid.NewGuid(), Name = "RoundOne" } })
            ));

        _mockMatchRepository
             .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
             .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Match>>>.Success(
                 new Tuple<int, IEnumerable<Match>>(0, new List<Match>
                 {
                 })
             ));

        _mockScoreRepository
            .Setup(repo => repo.SearchAllAsync(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Score>>>.Success(
                new Tuple<int, IEnumerable<Score>>(0, new List<Score>
                {
                    
                })
            ));

        var pageSize = 10;
        var pageNumber = 1;

        _mockMapper
            .Setup(mapper => mapper.Map<ScoreResultDTO>(It.IsAny<ScoreResult>()))
            .Returns((ScoreResult score) => new ScoreResultDTO
            {
                ParticipantId = score.ParticipantId,
                Faults = score.Faults,
                Time = score.Time,
                Id = score.Id,
                CompetitionId = competitionId
            });
        // Act
        var result = await _scoreService.GetResultsByCompetitionId(competitionId, pageSize, pageNumber);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value.Values);
    }
    
}
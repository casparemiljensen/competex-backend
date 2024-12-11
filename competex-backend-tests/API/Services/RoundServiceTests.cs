﻿using AutoMapper;
using Common.ResultPattern;
using competex_backend.API.DTOs;
using competex_backend.BLL.Services;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;
using Match = competex_backend.Models.Match;

namespace competex_backend_tests.API.Services
{
    public class RoundServiceTests
    {
        private readonly Mock<IRoundRepository> _roundRepositoryMock;
        private readonly Mock<IRegistrationRepository> _registrationRepositoryMock;
        private readonly Mock<IScoreRepository> _scoreRepositoryMock;
        private readonly Mock<IMatchRepository> _matchRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RoundService _roundService;

        public RoundServiceTests()
        {
            _roundRepositoryMock = new Mock<IRoundRepository>();
            _registrationRepositoryMock = new Mock<IRegistrationRepository>();
            _scoreRepositoryMock = new Mock<IScoreRepository>();
            _matchRepositoryMock = new Mock<IMatchRepository>();
            _mapperMock = new Mock<IMapper>();

            _roundService = new RoundService(
                _roundRepositoryMock.Object,
                _mapperMock.Object,
                _registrationRepositoryMock.Object,
                _scoreRepositoryMock.Object,
                _matchRepositoryMock.Object
            );
        }


        [Fact]
        public async Task GetByCompetitionId_ValidCompetitionId_ReturnsRounds()
        {
            // Arrange
            var competitionId = Guid.NewGuid();
            var rounds = new List<Round>
            {
                new Round { Id = Guid.NewGuid(), Name = "Shalom", CompetitionId = competitionId, SequenceNumber = 1 },
                new Round { Id = Guid.NewGuid(), Name = "Hasta la vista", CompetitionId = competitionId, SequenceNumber = 2 }
            };

            var roundDTOs = rounds.Select(r => new RoundDTO { Id = r.Id, SequenceNumber = r.SequenceNumber, Name = "Baby" }).ToList();

            _roundRepositoryMock
                .Setup(repo => repo.GetRoundIdsByCompetitionId(competitionId, null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(new Tuple<int, IEnumerable<Round>>(rounds.Count, rounds)));

            _mapperMock
                .Setup(mapper => mapper.Map<RoundDTO>(It.IsAny<Round>()))
                .Returns((Round src) => roundDTOs.First(r => r.Id == src.Id));

            // Act
            var result = await _roundService.GetByCompetitionId(competitionId, null, null);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(rounds.Count, result.Value.Item1);
            Assert.Equal(roundDTOs, result.Value.Item2);

            _roundRepositoryMock.Verify(repo => repo.GetRoundIdsByCompetitionId(competitionId, null, null), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<RoundDTO>(It.IsAny<Round>()), Times.Exactly(rounds.Count));
        }



        [Fact]
        public async Task CreateMatchesForRoundAsync_ValidData_CreatesMatches()
        {
            // Arrange
            var competitionId = Guid.NewGuid();
            var roundId = Guid.NewGuid();
            var roundSequenceNumber = 0;
            var participantIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var criteria = new CriteriaDTO { MaxFaults = 5, MaxMinutes = TimeSpan.FromMinutes(10) };

            var registrations = participantIds.Select(id => new Registration
            {
                ParticipantId = id,
                CompetitionId = competitionId,
                Status = RegistrationStatus.Accepted
            }).ToList();

            var rounds = new List<Round>
            {
                new Round { Id = roundId, Name = "Shalom2", CompetitionId = competitionId, SequenceNumber = (uint)roundSequenceNumber }
            };

            var matches = new List<Match>
            {
                new Match { Id = Guid.NewGuid(), RoundId = roundId, ParticipantIds = participantIds }
            };

            var scores = new List<Score>
            {
                new TimeFaultScore(2, TimeSpan.FromSeconds(10), matches[0].Id, participantIds[0]),
            };

            var matchDTOs = matches.Select(m => new MatchDTO { Id = m.Id, RoundId = m.RoundId }).ToList();

            _registrationRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Registration>>>.Success(new Tuple<int, IEnumerable<Registration>>(registrations.Count, registrations)));

            _roundRepositoryMock
                .Setup(repo => repo.GetRoundIdsByCompetitionId(competitionId, null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(new Tuple<int, IEnumerable<Round>>(rounds.Count, rounds)));

            _roundRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Round>>>.Success(new Tuple<int, IEnumerable<Round>>(rounds.Count, rounds)));

            _matchRepositoryMock
                .Setup(repo => repo.InsertAsync(It.IsAny<Match>()))
                .ReturnsAsync(ResultT<Guid>.Success(Guid.NewGuid()));

            _matchRepositoryMock
                .Setup(repo => repo.GetMatchesByRoundId(roundId, null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Match>>>.Success(new Tuple<int, IEnumerable<Match>>(matches.Count, matches)));
            _matchRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Match>>>.Success(new Tuple<int, IEnumerable<Match>>(matches.Count, matches)));

            _scoreRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null))
                .ReturnsAsync(ResultT<Tuple<int, IEnumerable<Score>>>.Success(new Tuple<int, IEnumerable<Score>>(scores.Count, scores)));

            _mapperMock
                .Setup(mapper => mapper.Map<MatchDTO>(It.IsAny<Match>()))
                .Returns((Match src) => matchDTOs.First(m => m.Id == src.Id));

            // Act
            var result = await _roundService.CreateMatchesForRoundAsync(competitionId, (uint)roundSequenceNumber, criteria, null, null);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(matches.Count, result.Value.Item1);
            Assert.Equal(matchDTOs, result.Value.Item2);

            _registrationRepositoryMock.Verify(repo => repo.GetAllAsync(null, null), Times.Once);
            _roundRepositoryMock.Verify(repo => repo.GetRoundIdsByCompetitionId(competitionId, null, null), Times.Once);
            _matchRepositoryMock.Verify(repo => repo.InsertAsync(It.IsAny<Match>()), Times.Exactly(1));//participantIds.Count * 2));
            _matchRepositoryMock.Verify(repo => repo.GetMatchesByRoundId(roundId, null, null), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<MatchDTO>(It.IsAny<Match>()), Times.Exactly(matches.Count));
        }


    }
}
using competex_backend.DAL.Repositories.PostgresDataAccess;
using competex_backend.Models;
using Moq;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace competex_backend_tests.API.DAL.Repositories.PostgresDataAccess
{
    public class PostgresGenericRepositoryTests
    {
        private readonly Mock<PostgresMemberRepository> _repository;

        public PostgresGenericRepositoryTests()
        {
            _repository = new Mock<PostgresMemberRepository>();
        }

        [Fact]
        public void BuildSearchQuery_WithNoFilters_ReturnsQueryVallidWhereClause()
        {
            // Arrange
            var filters = new Dictionary<string, object>()
            {
                { "id", new List<string>() { "idOne", "idTwo" } },
                { "competitionId", "competitionId" }
            };

            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);
            // Assert
            var expectedQuery = "SELECT * FROM \"Users\" WHERE (\"Id\" = {0} OR \"Id\" = {1}) AND (\"CompetitionId\" = {2})";
            Assert.Equal(expectedQuery, query);
            Assert.True(parameters.Count == 3);
        }

        [Fact]
        public void BuildSearchQuery_WithValidFilters_BuildsCorrectQueryAndParameters()
        {
            // Arrange
            var filters = new Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 30 },
            { "roles", new List<string> { "admin", "user" } }
        };

            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);
            // Assert
            var expectedQuery = "SELECT * FROM \"Users\" WHERE (\"Name\" = {0}) AND (\"Age\" = {1}) AND (\"Roles\" = {2} OR \"Roles\" = {3})";

            Assert.Equal(expectedQuery, query);
            Assert.Equal(4, parameters.Count);
            Assert.Equal("John", parameters[0].Value);
            Assert.Equal(30, parameters[1].Value);
            Assert.Equal("admin", parameters[2].Value);
            Assert.Equal("user", parameters[3].Value);
        }

        [Fact]
        public void BuildSearchQuery_WithInvalidSQLString_ThrowsException()
        {
            // Arrange
            var filters = new Dictionary<string, object>
        {
            { "na;me", "John" } // Invalid SQL string
        };
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters));
            Assert.Equal("Banned character used", ex.Message);
        }

        [Fact]
        public void BuildSearchQuery_WithNoFilters_ReturnsQueryWithoutWhereClause()
        {
            // Arrange
            var filters = new Dictionary<string, object>();

            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);

            // Assert
            var expectedQuery = "SELECT * FROM \"Users\"";
            Assert.Equal(expectedQuery, query);
            Assert.Empty(parameters);
        }

        [Fact]
        public void BuildSearchQuery_WithMultipleArrayFilters_BuildsCorrectQueryAndParameters()
        {
            // Arrange
            var filters = new Dictionary<string, object>
        {
            { "id", new List<string> { "idOne", "idTwo" } },
            { "competitionId", "competitionId" }
        };
            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);

            // Assert
            var expectedQuery = "SELECT * FROM \"Users\" WHERE (\"Id\" = {0} OR \"Id\" = {1}) AND (\"CompetitionId\" = {2})";
            Assert.Equal(expectedQuery, query);
            Assert.Equal(3, parameters.Count);
            Assert.Equal("idOne", parameters[0].Value);
            Assert.Equal("idTwo", parameters[1].Value);
            Assert.Equal("competitionId", parameters[2].Value);
        }

        [Fact]
        public void BuildSearchQuery_WithNullFilterValues_BuildsQueryIgnoringNullValues()
        {
            // Arrange
            var filters = new Dictionary<string, object>
        {
            { "name", null },
            { "age", 30 }
        };
            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);

            // Assert
            var expectedQuery = "SELECT * FROM \"Users\" WHERE (\"Age\" = {0})";
            Assert.Equal(expectedQuery, query);
            Assert.Single(parameters);
            Assert.Equal(30, parameters[0].Value);
        }

        [Fact]
        public void BuildSearchQuery_WithEmptyArrayFilter_IgnoresFilterInQuery()
        {
            // Arrange
            var filters = new Dictionary<string, object>
        {
            { "id", new List<string>() },
            { "competitionId", "competitionId" }
        };
            // Act
            var (query, parameters) = PostgresGenericRepository<Member>.BuildSearchQuery("Users", filters);

            // Assert
            var expectedQuery = "SELECT * FROM \"Users\" WHERE (\"CompetitionId\" = {0})";
            Assert.Equal(expectedQuery, query);
            Assert.Single(parameters);
            Assert.Equal("competitionId", parameters[0].Value);
        }
    }
}

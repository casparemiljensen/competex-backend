using Common.ResultPattern;
using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using competex_backend.Models;
using competex_backend_tests.API.Services;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

public class MockMemberRepositoryTests //: GenericServiceTests
{
    private readonly Mock<IMemberRepository> _repository;
    private readonly MockDatabaseManager mockDbManager;
    private readonly ITestOutputHelper _output;
    public MockMemberRepositoryTests(ITestOutputHelper output)
    {
        _output = output;
        mockDbManager = new MockDatabaseManager();
        _repository = new Mock<IMemberRepository>();
    }

    [Fact]
    public async Task GetById_ReturnsCorrectMember()
    {
        // Arrange
        var member = new Member
        {
            FirstName = "Alice",
            LastName = "Smith",
            Birthday = new DateTime(1990, 5, 14),
            Email = "alice.smith@example.com",
            Phone = "+1234567890",
            Permissions = 2,
        };

                

        // Call the test method with the mock repository
        //await GetById_ReturnsCorrectObject(repository, member);

        // Verify that methods were called
        // mockRepository.Verify(r => r.InsertAsync(It.IsAny<Member>()), Times.Once);  // Ensure InsertAsync was called once.
        // mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);  // Ensure GetByIdAsync was called once.
    }
}
using AutoMapper.Execution;
using Common.ResultPattern;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;
using Member = competex_backend.Models.Member;

namespace competex_backend_tests.API.Services
{
    public class GenericServiceTests
    {
        /*
        public async Task GetById_ReturnsCorrectObject<T, RType>(RType repository, T SampleObject, Guid? id = null) where RType : IGenericRepository<T> where T : class, IIdentifiable
        {
            ResultT<Guid> insertResult;
            // Arrange
            if (id == null)
            {
                insertResult = await repository.InsertAsync(SampleObject);
                Assert.True(insertResult.IsSuccess);
                Assert.True(insertResult.Value.GetType() == typeof(Guid));
                id = insertResult.Value;
            }

            var getResult = await repository.GetByIdAsync(id.GetValueOrDefault());
            Assert.True(getResult.IsSuccess);
            // Assert
            Assert.NotNull(getResult);
            Assert.Equal(id.GetValueOrDefault(), getResult.Value.Id);
            Assert.Equal(SampleObject, getResult.Value);
        }*/
        [Fact]
        public async Task GetById_ReturnsCorrectObject_WithMockRepository()
        {
            // Sample data to be used in the test
            var sampleMember = new Member
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Birthday = new DateTime(1990, 5, 14),
                Email = "john.doe@example.com",
                Phone = "+1234567890",
                Permissions = "User"
            };

            // Mock repository setup
            var mockRepository = new Mock<IGenericRepository<Member>>();

            // Mock InsertAsync to return success
            mockRepository.Setup(r => r.InsertAsync(It.IsAny<Member>()))
                .ReturnsAsync(ResultT<Guid>.Success(sampleMember.Id)); // Mocked to return the ID of the sampleMember.

            // Mock GetByIdAsync to return the sample member
            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ResultT<Member>.Success(sampleMember)); // Mocked to return the sampleMember.

            // Call the test method with the mock repository
            await GetById_ReturnsCorrectObjectTwo(mockRepository.Object, sampleMember);

            // Verify that methods were called
            mockRepository.Verify(r => r.InsertAsync(It.IsAny<Member>()), Times.Once);  // Ensure InsertAsync was called once.
            mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);  // Ensure GetByIdAsync was called once.
        }

        public async Task GetById_ReturnsCorrectObjectTwo<T, RType>(RType repository, T sampleObject, Guid? id = null)
            where RType : IGenericRepository<T>
            where T : class, IIdentifiable
        {
            ResultT<Guid> insertResult;
            // Arrange
            if (id == null)
            {
                insertResult = await repository.InsertAsync(sampleObject);
                Assert.True(insertResult.IsSuccess);
                Assert.True(insertResult.Value.GetType() == typeof(Guid));
                id = insertResult.Value;
            }

            var getResult = await repository.GetByIdAsync(id.GetValueOrDefault());
            Assert.True(getResult.IsSuccess);
            // Assert
            Assert.NotNull(getResult);
            Assert.Equal(id.GetValueOrDefault(), getResult.Value.Id);
            Assert.Equal(sampleObject, getResult.Value);
        }

        [Fact]
        public async Task MemberRepository_GetById_ReturnsCorrectMember()
        {
            // Arrange: Set up the application and services
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            // Start the application
            app.Start();

            // Get the registered services
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IMemberRepository>();

            // Insert a sample member
            var member = new Member
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Birthday = DateTime.UtcNow.AddYears(-30)
            };

            await repository.InsertAsync(member);

            // Act: Fetch the member by ID
            var result = await repository.GetByIdAsync(member.Id);

            // Assert: Verify the result
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(member.Id, result.Value.Id);
            Assert.Equal("John", result.Value.FirstName);
        }
    }
}
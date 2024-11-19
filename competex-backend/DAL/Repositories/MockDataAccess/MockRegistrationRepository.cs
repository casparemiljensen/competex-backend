using System;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockRegistrationRepository : MockGenericRepository<Registration>, IRegistrationRepository
{
    public MockRegistrationRepository(MockDatabaseManager db) : base(db)
    {
    }
}

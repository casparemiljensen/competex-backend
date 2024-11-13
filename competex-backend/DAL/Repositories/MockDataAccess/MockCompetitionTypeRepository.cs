using System;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess;

public class MockCompetitionTypeRepository : MockGenericRepository<CompetitionType>, ICompetitionTypeRepository
{
    public MockCompetitionTypeRepository(MockDatabaseManager db) : base(db)
    {
    }
}

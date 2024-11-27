﻿using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.MockDataAccess
{

    public class MockParticipantRepository : MockGenericRepository<Participant>, IParticipantRepository
    {
        public MockParticipantRepository(MockDatabaseManager db)
            : base(db)
        {
        }
    }
}
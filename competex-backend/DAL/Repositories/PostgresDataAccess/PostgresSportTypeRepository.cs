﻿using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.DAL.Repositories.PostgresDataAccess
{
    public class PostgresSportTypeRepository : PostgresGenericRepository<SportType>, ISportTypeRepository
    {
        public PostgresSportTypeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}


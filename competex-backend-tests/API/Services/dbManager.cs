using competex_backend.DAL.Interfaces;
using competex_backend.DAL.Repositories.MockDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace competex_backend_tests.API.Services
{
    internal class dbManager
    {
        public static MockDatabaseManager db = new MockDatabaseManager();
        public ICompetitionTypeRepository CompetitionTypeRepository { get; set; } = new MockCompetitionTypeRepository(db);
        public ICompetitionRepository CompetitionRepository { get; set; }
    }
}

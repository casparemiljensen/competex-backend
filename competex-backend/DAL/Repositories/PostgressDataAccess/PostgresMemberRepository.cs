using competex_backend.Models;
using competex_backend.DAL.Interfaces;

namespace competex_backend.DAL.Repositories.PostgressDataAccess
{
    internal class PostgresMemberRepository : IMemberRepository
    {
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetAll()
        {
            throw new NotImplementedException();
        }

        public Member? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(Member obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Member obj)
        {
            throw new NotImplementedException();
        }
    }
}

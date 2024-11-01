using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IDatabaseManager<T> where T : class
    {
        //List<T> Entities { get; set; }  // Generic list of entities of type T

        //List<Member> Members { get; set; }
        //List<ClubMember> ClubMembers { get; set; }
        //List<Entity> Entities { get; set; }

        List<T> Entities { get; set; }  // Generic list of entities of type T

        //List<T> GetEntities<T>() where T : class;

    }
}

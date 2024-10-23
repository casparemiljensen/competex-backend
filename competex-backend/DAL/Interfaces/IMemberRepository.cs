using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMemberRepository
    {
        // Retrieve all members
        List<Member> GetMembers();

        // Retrieve a specific member by ID
        Member GetMemberById(Guid memberId);

        // Add a new member
        void AddMember(Member member);

        // Update an existing member
        void UpdateMember(Member member);

        // Delete a member by ID
        void DeleteMember(Guid memberId);

    }
}

using competex_backend.Models;

namespace competex_backend.DAL.Interfaces
{
    public interface IMemberRepository
    {
        // Retrieve all members asynchronously
        Task<List<Member>> GetMembersAsync();

        // Retrieve a specific member by ID asynchronously
        Task<Member?> GetMemberByIdAsync(Guid memberId);

        // Add a new member asynchronously
        Task<bool> AddMemberAsync(Member member);

        // Update an existing member asynchronously
        Task<bool> UpdateMemberAsync(Member member);

        // Delete a member by ID asynchronously
        Task<bool> DeleteMemberAsync(Guid memberId);

    }
}

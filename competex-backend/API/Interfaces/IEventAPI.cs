using competex_backend.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Interfaces
{
    public interface IEventAPI : IGenericAPI<EventDTO>
    {
        public Task<IActionResult> GetMembersOwedAmount(Guid eventId, Guid memberId);
    }
}

using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : GenericsController<EventDTO>
    {
        private readonly IEventService _eventService;
        public EventsController(IGenericService<EventDTO> service, IEventService eventService) : base(service)
        {
            _eventService = eventService;
        }

        [HttpGet("{eventId}GetMemberOwedAmount/{memberId}")]
        public async Task<IActionResult> GetMembersOwedAmount(Guid eventId, Guid memberId)
        {
            var result = await _eventService.GetMembersOwedAmount(memberId, eventId);
            if (result.IsSuccess)
            {
                return Ok(new Dictionary<string, int> { { "amount", result.Value } });
            }
            return NotFound(result.Error); // Return NotFound with error details if no members found
        }

        [HttpPost("{eventId}addCompetition/{competitionId}")]
        public async Task<IActionResult> AddCompetition(Guid eventId, Guid competitionId)
        {
            var result = await _eventService.AddCompetition(eventId, competitionId);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound(result.Error); // Return NotFound with error details if no members found
        }
    }
}

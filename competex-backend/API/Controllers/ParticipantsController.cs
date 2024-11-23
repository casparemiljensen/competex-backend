using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : GenericsController<ParticipantDTO>, IParticipantAPI
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IGenericService<ParticipantDTO> service, IParticipantService participantService) : base(service)
        {
            _participantService = participantService;
        }


        [HttpPost()]
        public override async Task<IActionResult> CreateAsync([FromBody] ParticipantDTO participant)
        {
            var processedParticipant = ProcessParticipant(participant);
            if (processedParticipant == null)
                return BadRequest("Invalid participant type.");

            var result = await _participantService.CreateAsync(processedParticipant);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ParticipantDTO participant)
        {
            var processedParticipant = ProcessParticipant(participant);
            if (processedParticipant == null)
                return BadRequest("Invalid participant type.");

            var result = await _participantService.UpdateAsync(id, processedParticipant);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        private ParticipantDTO? ProcessParticipant(ParticipantDTO participant)
        {
            return participant switch
            {
                TeamCreateUpdateDTO teamParticipant => new TeamCreateUpdateDTO
                {
                    Name = teamParticipant.Name,
                    MemberIds = teamParticipant.MemberIds?? new List<Guid>() // Default to empty list if null
                },
                SingleCreateUpdateDTO singleParticipant => new SingleCreateUpdateDTO
                {
                    Name = singleParticipant.Name,
                    MemberId = singleParticipant.MemberId
                },
                EkvipageCreateUpdateDTO ekvipageParticipant => new EkvipageCreateUpdateDTO
                {
                    Name = ekvipageParticipant.Name,
                    MemberId = ekvipageParticipant.MemberId,
                    EntityId = ekvipageParticipant.EntityId
                },
                _ => null // Invalid type
            };
        }
    }
}

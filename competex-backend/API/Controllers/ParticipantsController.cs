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
                TeamDTO teamParticipant => new TeamDTO
                {
                    Name = teamParticipant.Name,
                    Members = teamParticipant.Members ?? new List<MemberDTO>() // Default to empty list if null
                },
                SingleDTO singleParticipant => new SingleDTO
                {
                    Name = singleParticipant.Name,
                    Member = singleParticipant.Member
                        ?? throw new InvalidOperationException("Single participant must have a member.")
                },
                EkvipageDTO ekvipageParticipant => new EkvipageDTO
                {
                    Name = ekvipageParticipant.Name,
                    Member = ekvipageParticipant.Member
                        ?? throw new InvalidOperationException("Ekvipage must have a member."),
                    Entity = ekvipageParticipant.Entity
                        ?? throw new InvalidOperationException("Ekvipage must have an entity.")
                },
                _ => null // Invalid type
            };
        }
    }
}

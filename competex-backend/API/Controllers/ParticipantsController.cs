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


        [HttpPost("custom")]
        public override async Task<IActionResult> CreateAsync([FromBody] ParticipantDTO participant)
        {

            ParticipantDTO processedParticipant = participant.Type switch
            {
                "Team" => participant is TeamDTO teamParticipant
                    ? new TeamDTO()
                    {
                        Name = teamParticipant.Name,
                        Members = teamParticipant.Members ?? new List<MemberDTO>() // Default to empty list if null
                    }
                    : throw new InvalidCastException("Participant is not of type TeamDTO"),

                "Single" => participant is SingleDTO singleParticipant
                    ? new SingleDTO
                    {
                        Name = singleParticipant.Name,
                        Member = singleParticipant.Member
                            ?? throw new InvalidOperationException("Single participant must have a member.")
                    }
                    : throw new InvalidCastException("Participant is not of type SingleDTO"),

                "Ekvipage" => participant is EkvipageDTO ekvipageParticipant
                    ? new EkvipageDTO
                    {
                        Name = ekvipageParticipant.Name,
                        Member = ekvipageParticipant.Member
                            ?? throw new InvalidOperationException("Ekvipage must have a member."),
                        Entity = ekvipageParticipant.Entity
                            ?? throw new InvalidOperationException("Ekvipage must have an entity.")
                    }
                    : throw new InvalidCastException("Participant is not of type EkvipageDTO"),

                _ => throw new ArgumentException($"Invalid participant type: {participant.Type}")
            };

            var result = await _participantService.CreateAsync(processedParticipant);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}

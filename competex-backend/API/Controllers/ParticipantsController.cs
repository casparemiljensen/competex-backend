using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Models;
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
            // Perform custom logic here
            var type = participant.Type;

            // Example logic for processing participant
            ParticipantDTO processedParticipant = type switch
            {
                "Team" => new TeamDTO { /* Assign values */ },
                "Single" => new SingleDTO { /* Assign values */ },
                "Ekvipage" => new EkvipageDTO { /* Assign values */ },
                _ => throw new ArgumentException($"Invalid participant type: {type}")
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

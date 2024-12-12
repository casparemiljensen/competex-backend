using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : GenericsController<EkvipageDTO>, IParticipantAPI
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IGenericService<EkvipageDTO> service, IParticipantService participantService) : base(service)
        {
            _participantService = participantService;
        }
    }
}

using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltiesController : GenericsController<PenaltyDTO>, IPenaltyAPI
    {
        private IPenaltyService _penaltyService;

        public PenaltiesController(IGenericService<PenaltyDTO> service, IPenaltyService penaltyService) : base(service)
        {
            _penaltyService = penaltyService;
        }
    }
}

using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : GenericsController<ClubDTO>
    {
        private readonly IClubService _clubService;

        public ClubsController(IGenericService<ClubDTO> service, IClubService clubService) : base(service)
        {
            _clubService = clubService;
        }
    }
}
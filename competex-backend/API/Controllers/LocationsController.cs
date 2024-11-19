using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : GenericsController<LocationDTO>, ILocationAPI
    {
        private ILocationService _locationService;

        public LocationsController(IGenericService<LocationDTO> service, ILocationService locationService) : base(service)
        {
            _locationService = locationService;
        }
    }
}

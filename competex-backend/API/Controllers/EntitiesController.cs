using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : GenericsController<EntityDTO>, IEntityAPI
    {
        public EntitiesController(IGenericService<EntityDTO> service) : base(service) { }

    }
}

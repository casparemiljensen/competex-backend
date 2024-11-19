using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : GenericsController<RegistrationDTO>
    {
        public RegistrationController(IGenericService<RegistrationDTO> service) : base(service) { }

    }
}

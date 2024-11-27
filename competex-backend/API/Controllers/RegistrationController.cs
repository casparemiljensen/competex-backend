using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : GenericsController<RegistrationDTO>, IRegistrationAPI
    {
        private IRegistrationService _registrationService;

        public RegistrationController(IGenericService<RegistrationDTO> service, IRegistrationService registrationService) : base(service) 
        {
            _registrationService = registrationService;
        }
    }
}

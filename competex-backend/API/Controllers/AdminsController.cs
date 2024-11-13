using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : GenericsController<AdminDTO>, IAdminAPI
    {
        private IAdminService _adminService;

        public AdminsController(IGenericService<AdminDTO> service, IAdminService adminService) : base(service)
        {
            _adminService = adminService;
        }
    }
}

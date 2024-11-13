using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.Common.Helpers;
using competex_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportTypeController : GenericsController<SportTypeDTO>, ISportTypeAPI
    {
        private ISportTypeService _sportTypeService;

        public SportTypeController(IGenericService<SportTypeDTO> service, ISportTypeService sportTypeService) : base(service)
        {
            _sportTypeService = sportTypeService;
        }
    }
}

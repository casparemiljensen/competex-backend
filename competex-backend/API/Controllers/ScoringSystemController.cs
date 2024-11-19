using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringSystemController : GenericsController<ScoringSystemDTO>
    {
        public ScoringSystemController(IGenericService<ScoringSystemDTO> service) : base(service) { }

    }
}

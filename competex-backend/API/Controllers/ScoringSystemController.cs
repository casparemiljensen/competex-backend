using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringSystemController : GenericsController<ScoringSystemDTO>, IScoringSystemAPI
    {
        private IScoringSystemService _scoringSystemService;

        public ScoringSystemController(IGenericService<ScoringSystemDTO> service, IScoringSystemService scoringSystemService) : base(service) 
        {
            _scoringSystemService = scoringSystemService;
        }
    }
}

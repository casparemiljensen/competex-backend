using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : GenericsController<ScoreDTO>, IScoreAPI
    {
        private IScoreService _scoreService;
        public ScoresController(IGenericService<ScoreDTO> service, IScoreService scoreService) : base(service)
        {
            _scoreService = scoreService;
        }
    }
}

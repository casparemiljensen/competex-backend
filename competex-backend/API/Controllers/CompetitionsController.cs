using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionsController : GenericsController<CompetitionDTO>, ICompetitionAPI
    {
        private ICompetitionService _competitionService;

        public CompetitionsController(IGenericService<CompetitionDTO> service, ICompetitionService competitionService) : base(service)
        {
            _competitionService = competitionService;
        }
    }
}

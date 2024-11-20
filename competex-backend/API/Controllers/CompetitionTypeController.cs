using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionTypeController : GenericsController<CompetitionTypeDTO>, ICompetitionTypeAPI
    {
        private ICompetitionTypeService _competitionTypeService;

        public CompetitionTypeController(IGenericService<CompetitionTypeDTO> service, ICompetitionTypeService competitionTypeService) : base(service)
        {
            _competitionTypeService = competitionTypeService;
        }
    }
}

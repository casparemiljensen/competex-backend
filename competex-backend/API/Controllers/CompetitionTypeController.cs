using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

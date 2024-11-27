using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgesController : GenericsController<JudgeDTO>
    {
        private readonly IJudgeService _judgeService;

        public JudgesController(IGenericService<JudgeDTO> service, IJudgeService judgeService) : base(service)
        {
            _judgeService = judgeService;
        }
    }
}
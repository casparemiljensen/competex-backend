using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;

namespace competex_backend.API.Controllers
{
    public class JudgesController : GenericsController<JudgeDTO>
    {
        private readonly IJudgeService _judgeService;
        public JudgesController(IGenericService<JudgeDTO> service, IJudgeService judgeService) : base(service)
        {
            _judgeService = judgeService;
        }
    }
}
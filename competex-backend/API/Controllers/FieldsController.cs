using competex_backend.API.DTOs;
using competex_backend.API.Interfaces;
using competex_backend.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competex_backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : GenericsController<FieldDTO>, IFieldAPI
    {
        private IFieldService _fieldService;

        public FieldsController(IGenericService<FieldDTO> service, IFieldService fieldService) : base(service)
        {
            _fieldService = fieldService;
        }
    }
}

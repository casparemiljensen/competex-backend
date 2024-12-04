using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class FieldService : GenericService<Field, FieldDTO>, IFieldService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;

        public FieldService(IGenericRepository<Field> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _fieldRepository = (IFieldRepository)repository;
            _mapper = mapper;
        }
    }
}
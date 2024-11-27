using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class EntityService : GenericService<Entity, EntityDTO>, IEntityService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMapper _mapper;

        public EntityService(IGenericRepository<Entity> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _entityRepository = (IEntityRepository)repository;
            _mapper = mapper;
        }
    }
}

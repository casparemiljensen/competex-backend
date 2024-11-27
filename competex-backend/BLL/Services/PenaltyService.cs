using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class PenaltyService : GenericService<Penalty, PenaltyDTO>, IPenaltyService
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IMapper _mapper;

        public PenaltyService(IGenericRepository<Penalty> repository, IMapper mapper)
    : base(repository, mapper)
        {
            _penaltyRepository = (IPenaltyRepository)repository;
            _mapper = mapper;
        }
    }
}

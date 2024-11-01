using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ClubService : GenericService<Club, ClubDTO>, IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public ClubService(IGenericRepository<Club> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _clubRepository = (IClubRepository)repository;
            _mapper = mapper;
        }
    }
}

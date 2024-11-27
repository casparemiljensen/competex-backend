using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class LocationService : GenericService<Location, LocationDTO>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(IGenericRepository<Location> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _locationRepository = (ILocationRepository)repository;
            _mapper = mapper;
        }
    }
}

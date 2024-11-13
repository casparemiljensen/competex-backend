using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.BLL.Services;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class EventService : GenericService<Event, EventDTO>, IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IGenericRepository<Event> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _eventRepository = (IEventRepository)repository;
            _mapper = mapper;
        }
    }
}
﻿using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{
    public class ParticipantService : GenericService<Participant, ParticipantDTO>, IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantService(IGenericRepository<Participant> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _participantRepository = (IParticipantRepository)repository;
            _mapper = mapper;
        }
    }

   
}

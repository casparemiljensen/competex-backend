using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using System.Net.Security;

namespace competex_backend.BLL.Services
{
    public class CompetitionService : GenericService<Competition, CompetitionDTO>, ICompetitionService
    {
        private readonly IMapper _mapper;

        public CompetitionService(IGenericRepository<Competition> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _mapper = mapper;
        }
    }
}

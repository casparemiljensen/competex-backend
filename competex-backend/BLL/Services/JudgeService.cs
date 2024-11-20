using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{

    public class JudgeService : GenericService<Judge, JudgeDTO>, IJudgeService
    {
        private readonly IJudgeRepository _judgeRepository;
        private readonly IMapper _mapper;

        public JudgeService(IGenericRepository<Judge> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _judgeRepository = (IJudgeRepository)repository;
            _mapper = mapper;
        }
    }
}

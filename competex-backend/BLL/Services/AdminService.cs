using AutoMapper;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;

namespace competex_backend.BLL.Services
{

    public class AdminService : GenericService<Admin, AdminDTO>, IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IGenericRepository<Admin> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _adminRepository = (IAdminRepository)repository;
            _mapper = mapper;
        }
    }
}

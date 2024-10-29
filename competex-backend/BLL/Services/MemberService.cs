﻿using AutoMapper;
using AutoMapper.Execution;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;
using competex_backend.Models;
using Member = competex_backend.Models.Member;

namespace competex_backend.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public MemberDTO? GetById(Guid id)
        {
            var member = _memberRepository.GetByIdAsync(id).Result;
            if (member == null)
                return null;
            return _mapper.Map<MemberDTO>(member);
        }

        public IEnumerable<MemberDTO> GetAll()
        {
            var members = _memberRepository.GetAllAsync().Result;
            // Map Member to MemberDto
            var memberDtos = new List<MemberDTO>();
            foreach (var member in members)
            {
                memberDtos.Add(_mapper.Map<MemberDTO>(member));
            }
            return memberDtos; // Return the list of DTOs
        }

        public bool Create(MemberDTO obj)
        {
            // Map MemberDto to Member
            var member = _mapper.Map<Member>(obj);
            _memberRepository.InsertAsync(member);
            return true;
        }

        public bool Update(MemberDTO obj)
        {
            var member = _mapper.Map<Member>(obj);
            _memberRepository.UpdateAsync(member);
            return true;
        }

        public bool Remove(Guid id)
        {
            _memberRepository.DeleteAsync(id);
            return true;
        }
    }
}

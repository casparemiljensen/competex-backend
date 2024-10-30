using AutoMapper;
using AutoMapper.Execution;
using Common.ResultPattern;
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

        public async Task<ResultT<MemberDTO>> GetByIdAsync(Guid id)
        {
            var result = await _memberRepository.GetByIdAsync(id);
            if (result.IsSuccess && result.Value != null)
            {
                return ResultT<MemberDTO>.Success(_mapper.Map<MemberDTO>(result.Value));
            }
            return ResultT<MemberDTO>.Failure(result.Error ?? Error.Failure("UnknownError", "An unknown error occurred."));
        }

        public async Task<ResultT<IEnumerable<MemberDTO>>> GetAllAsync()
        {
            var result = await _memberRepository.GetAllAsync();
            var memberDtos = result.Value.Select(m => _mapper.Map<MemberDTO>(m)).ToList();
            return ResultT<IEnumerable<MemberDTO>>.Success(memberDtos);
        }

        public async Task<ResultT<Guid>> CreateAsync(MemberDTO obj)
        {
            var member = _mapper.Map<Member>(obj);
            var result = await _memberRepository.InsertAsync(member);
            if (result.IsSuccess)
            {
                return ResultT<Guid>.Success(result.Value);
            }
            return ResultT<Guid>.Failure(result.Error ?? Error.Validation("CreationFailed", "Failed to create member."));
        }

        public async Task<Result> UpdateAsync(MemberDTO obj)
        {
            var member = _mapper.Map<Member>(obj);
            var result = await _memberRepository.UpdateAsync(member);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.Error ?? Error.Validation("UpdateFailed", "Failed to update member."));
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var result = await _memberRepository.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Result.Success();
            }
            return Result.Failure(result.Error ?? Error.Validation("DeletionFailed", "Failed to delete member."));
        }
    }
}

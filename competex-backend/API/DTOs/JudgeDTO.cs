using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class JudgeDTO : Identifiable
    {
        public JudgeType JudgeType { get; set; }
        public MemberDTO? Member { get; set; }
        public Guid? MemberId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

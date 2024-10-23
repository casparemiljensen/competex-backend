namespace competex_backend.API.DTOs
{
    //@APIModel
    public class MemberDTO
    {
        public Guid MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

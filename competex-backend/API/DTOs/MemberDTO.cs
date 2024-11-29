using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class MemberDTO : Identifiable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Permissions Permissions { get; set; } = 0; //Set to correct type when we figure out how to handle permissions
    }
}

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class MemberDTO
    {
        //TODO: Figure out how to handle this.. We do not want it for create or update, but for get.
        //[JsonIgnore]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty; //Set to correct type when we figure out how to handle permissions
    }
}

namespace competex_backend.Models

{
    public class Member
    {
        public Guid MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Permissions { get; set; } //Set to correct type when we figure out how to handle permissions
        
        // No club implementation yet. 

        public Member(string firstName, string lastName)
        {
            MemberId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public Member(string firstName, string lastName, Guid memberId)
            : this(firstName, lastName)
        {
            MemberId = memberId;
        }

    }
}

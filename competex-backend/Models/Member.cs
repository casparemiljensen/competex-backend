namespace competex_backend.Models

{
    public class Member : IIdentifiable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Permissions { get; set; } //Set to correct type when we figure out how to handle permissions
        // public ICollection<ClubMember> ClubMembers { get; set; } = new List<ClubMember>();


        // No club implementation yet. 

        public Member(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }

        public Member(string firstName, string lastName, Guid id)
            : this(firstName, lastName)
        {
            Id = id;
        }

    }
}

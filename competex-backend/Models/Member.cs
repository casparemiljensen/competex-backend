namespace competex_backend.Models

{
    public class Member : IIdentifiable
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;

        public DateTime Birthday { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Permissions { get; set; } = String.Empty; //Set to correct type when we figure out how to handle permissions
        // public ICollection<ClubMember> ClubMembers { get; set; } = new List<ClubMember>();


        // No club implementation yet. 

        public Member()
        {
            Id = Guid.NewGuid();
        }
        public Member(Guid id)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

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

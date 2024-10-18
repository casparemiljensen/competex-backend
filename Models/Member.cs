namespace competex_backend.Models

{
    public class Member
    {
        public Guid MemberId { get; set; }
        public string Name { get; set; }

        public Member(string name)
        {
            MemberId = Guid.NewGuid();
            Name = name;
        }

    }
}

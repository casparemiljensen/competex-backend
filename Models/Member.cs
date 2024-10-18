namespace competex_backend.Models

{
    public class Member
    {
        public Guid MemberId { get; private set; }
        public string Name { get; private set; }

        public Member(string name)
        {
            MemberId = Guid.NewGuid();
            Name = name;
        }

    }
}

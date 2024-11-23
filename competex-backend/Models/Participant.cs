namespace competex_backend.Models
{
    public abstract class Participant : Identifiable
    {
        public string Name { get; set; }

        public Participant(string name)
        {
            Name = name;
        }
    }

    public class Team : Participant
    {
        public List<Guid> MemberIds { get; set; }
        public Team(string name, List<Guid> memberIds) : base(name)
        {
            MemberIds = memberIds;
        }
    }

    public class Single : Participant
    {
        public Guid MemberId { get; set; }
        public Single(string name, Guid memberId) : base(name)
        {
            MemberId = memberId;
        }
    }

    public class Ekvipage : Participant
    {
        public Guid MemberId { get; set; }
        public Guid EntityId { get; set; }
        public Ekvipage(string name, Guid memberId, Guid entityId) : base(name)
        {
            MemberId = memberId;
            EntityId = entityId;
        }
    }
}
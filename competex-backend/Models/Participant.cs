namespace competex_backend.Models
{
    public abstract class Participant : IIdentifiable
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    internal class Team : Participant
    {
        public List<Member> Members { get; set; }
        public Team(string name, List<Member> members) : base()
        {
            Name = name;
            Members = members;
        }

        //public void AddMember(Member member)
        //{
        //    this.members.Add(member);
        //    EvaluteIsPlayer();
        //}

        //private void EvaluteIsPlayer()
        //{
        //    isPlayer = members.Count < 2;
        //}
    }

    internal class Single : Participant
    {
        public Member Member { get; set; }
        public Single(string name, Member member) : base()
        {
            Name = name;
            Member = member;
        }
    }

    internal class Ekvipage : Participant
    {
        public Member Member { get; set; }
        public Entity Entity { get; set; }
        public Ekvipage(string name, Member member, Entity entity) : base()
        {
            Name = name;
            Member = member;
            Entity = entity;
        }
    }
}
using AutoMapper.Execution;

namespace competex_backend.Models
{
    public class Participant
    {
        public Guid ParticipantId { get; set; }
        public string Name { get; set; }

        public Participant(string name)
        {
            ParticipantId = Guid.NewGuid();
            Name = name;
        }
    }

    internal class Team : Participant
    {
        public List<Member> Members { get; set; }
        public Team(string name, List<Member> members) : base(name)
        {
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
        public Single(string name, Member member) : base(name)
        {
            Member = member;
        }
    }

    internal class Ekvipage : Participant
    {
        public Member Member { get; set; }
        public Entity Entity { get; set; }
        public Ekvipage(string name, Member member, Entity entity) : base(name)
        {
            Member = member;
            Entity = entity;
        }
    }
}
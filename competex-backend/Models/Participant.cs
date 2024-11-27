﻿namespace competex_backend.Models
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
        public List<Member> Members { get; set; }
        public Team(string name, List<Member> members) : base(name)
        {
            Members = members;
        }
    }

    public class Single : Participant
    {
        public Member Member { get; set; }
        public Single(string name, Member member) : base(name)
        {
            Member = member;
        }
    }

    public class Ekvipage : Participant
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
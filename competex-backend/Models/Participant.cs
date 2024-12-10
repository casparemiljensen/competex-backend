using System.ComponentModel.DataAnnotations.Schema;

namespace competex_backend.Models
{
    public abstract class Participant : Identifiable
    {
        public string Name { get; set; }
        public short ParticipantType { get; set; } // Maps to smallint in DB
                                                   // Navigation property for ParticipantMember
                                                   

        public Participant(string name, short participantType)
        {
            Name = name;
            ParticipantType = participantType;
        }
    }

    // 1: Team, 2: Single, 3: Ekvipage

    public class Team : Participant
    {
        public List<Guid> MemberIds { get; set; }
        // Parameterless constructor required by EF Core
        private Team() : base(string.Empty, 1) { }
        public Team(string name, List<Guid> memberIds) : base(name, 1)
        {
            MemberIds = memberIds;
        }
    }

    public class Single : Participant
    {
        public Guid MemberId { get; set; }
        // Parameterless constructor required by EF Core
        private Single() : base(string.Empty, 2) { }
        public Single(string name, Guid memberId) : base(name, 2)
        {
            MemberId = memberId;
        }
    }

    //public class Ekvipage : Participant
    //{
    //    public Guid MemberId { get; set; }
    //    public Guid EntityId { get; set; }

    //    //Parameterless constructor required by EF Core
    //    private Ekvipage() : base(string.Empty, 3) { }

    //    public Ekvipage(string name, Guid memberId, Guid entityId) : base(name, 3)
    //    {
    //        MemberId = memberId;
    //        EntityId = entityId;
    //    }
    //}

    public class Ekvipage : Identifiable
    {
        public string Name { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? EntityId { get; set; }

        private Ekvipage() { }

        public Ekvipage(string name, Guid memberId, Guid entityId)
        {
            MemberId = memberId;
            EntityId = entityId;
        }
    }
}
using JsonSubTypes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    [JsonDerivedType(typeof(TeamDTO), typeDiscriminator: "Team")]
    [JsonDerivedType(typeof(SingleDTO), typeDiscriminator: "Single")]
    [JsonDerivedType(typeof(EkvipageDTO), typeDiscriminator: "Ekvipage")]

    public abstract class ParticipantDTO : Identifiable
    {
        public string Name { get; set; }  // Shared property
        public string Type { get; set; }  // Discriminator (e.g., "Team", "Single", "Ekvipage")
    }

    public class TeamDTO : ParticipantDTO
    {
        public List<MemberDTO> Members { get; set; }
    }

    public class SingleDTO : ParticipantDTO
    {
        public MemberDTO Member { get; set; }
    }

    public class EkvipageDTO : ParticipantDTO
    {
        public MemberDTO Member { get; set; }
        public EntityDTO Entity { get; set; }
    }
}

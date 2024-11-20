using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    [JsonPolymorphic()]
    [JsonDerivedType(typeof(TeamDTO), typeDiscriminator: "Team")]
    [JsonDerivedType(typeof(SingleDTO), typeDiscriminator: "Single")]
    [JsonDerivedType(typeof(EkvipageDTO), typeDiscriminator: "Ekvipage")]
    [SwaggerDiscriminator("$type")]
    [SwaggerSubType(typeof(TeamDTO), DiscriminatorValue = "Team")]
    [SwaggerSubType(typeof(SingleDTO), DiscriminatorValue = "Single")]
    [SwaggerSubType(typeof(EkvipageDTO), DiscriminatorValue = "Ekvipage")]
    public abstract class ParticipantDTO : Identifiable
    {
        public string Name { get; set; }

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

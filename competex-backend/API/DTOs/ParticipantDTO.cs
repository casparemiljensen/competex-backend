using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    [JsonPolymorphic()]
    [JsonDerivedType(typeof(TeamCreateUpdateDTO), typeDiscriminator: "TeamCreateUpdate")]
    [JsonDerivedType(typeof(TeamDetailDTO), typeDiscriminator: "TeamDetail")]

    [JsonDerivedType(typeof(SingleDetailDTO), typeDiscriminator: "Single")]
    [JsonDerivedType(typeof(EkvipageDetailDTO), typeDiscriminator: "Ekvipage")]

    [SwaggerDiscriminator("$type")]
    [SwaggerSubType(typeof(TeamCreateUpdateDTO), DiscriminatorValue = "TeamCreateUpdate")]
    [SwaggerSubType(typeof(TeamDetailDTO), DiscriminatorValue = "TeamDetail")]
    [SwaggerSubType(typeof(SingleDetailDTO), DiscriminatorValue = "Single")]
    [SwaggerSubType(typeof(EkvipageDetailDTO), DiscriminatorValue = "Ekvipage")]
    public abstract class ParticipantDTO : Identifiable
    {
        public string Name { get; set; }
    }

    public class TeamDetailDTO : ParticipantDTO
    {
        public List<MemberDTO> Members { get; set; }
    }

    public class TeamCreateUpdateDTO : ParticipantDTO
    {
        public List<Guid> MemberIds { get; set; }
    }

    public class SingleDetailDTO : ParticipantDTO
    {
        public MemberDTO Member { get; set; }
    }

    public class SingleCreateUpdateDTO : ParticipantDTO
    {
        public Guid MemberId { get; set; }
    }

    public class EkvipageDetailDTO : ParticipantDTO
    {
        public MemberDTO Member { get; set; }
        public EntityDTO Entity { get; set; }
    }

    public class EkvipageCreateUpdateDTO : ParticipantDTO
    {
        public Guid MemberId { get; set; }
        public Guid EntityId { get; set; }
    }
}

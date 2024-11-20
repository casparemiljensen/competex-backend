using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
    [JsonDerivedType(typeof(TeamDTO), typeDiscriminator: "Team")]
    [JsonDerivedType(typeof(SingleDTO), typeDiscriminator: "Single")]
    [JsonDerivedType(typeof(EkvipageDTO), typeDiscriminator: "Ekvipage")]
    [SwaggerDiscriminator("type")]
    [SwaggerSubType(typeof(TeamDTO), DiscriminatorValue = "Team")]
    [SwaggerSubType(typeof(SingleDTO), DiscriminatorValue = "Single")]
    [SwaggerSubType(typeof(EkvipageDTO), DiscriminatorValue = "Ekvipage")]
    public abstract class ParticipantDTO : Identifiable
    {
        [JsonConstructor]
        public ParticipantDTO() {}
        public string Name { get; set; }  // Shared property
        [JsonPropertyName("type")]
        public string Type { get; set; }  // Discriminator (e.g., "Team", "Single", "Ekvipage")

    }

    public class TeamDTO : ParticipantDTO
    {
        [JsonConstructor]
        public TeamDTO() { }

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

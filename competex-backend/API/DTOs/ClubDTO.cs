using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    public class ClubDTO
    {
        public Guid Id { get; set; }
        public string Clubname { get; set; } = string.Empty;
        public string AssociatedSport { get; set; } = string.Empty;
    }
}

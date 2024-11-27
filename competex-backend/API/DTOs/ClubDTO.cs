using competex_backend.Models;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    public class ClubDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public string AssociatedSport { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace competex_backend.Models
{
    public class Penalty : Identifiable
    {
        public PenaltyType PenaltyType { get; set; }

        [NotMapped] // Ignore this property in EF Core - for now...
        public object? PenaltyValue { get; set; }
    }
}

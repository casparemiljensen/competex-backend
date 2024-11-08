using System.ComponentModel.DataAnnotations;

namespace competex_backend.Models
{
    public interface IIdentifiable
    {
        [Required]
        Guid Id { get; set; }
    }
}

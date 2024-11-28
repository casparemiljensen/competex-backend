
namespace competex_backend.Models
{
    public class Admin : Member
    {
        public required List<Guid> SportTypeIds { get; set; }

        public Admin() : base()
        {

        }
    }
}

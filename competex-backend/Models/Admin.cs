
namespace competex_backend.Models
{
    public class Admin : Member
    {
        public required List<SportType> SportTypes { get; set; }

        public Admin() : base()
        {

        }
    }
}

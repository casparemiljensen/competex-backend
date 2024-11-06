
namespace competex_backend.Models
{
    public class Admin : Member
    {
        public Guid MemberId { get; set; }
        public required SportType SportType { get; set; }

        public Admin() : base()
        {

        }
    }
}

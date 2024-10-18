using competex_backend.Models;
using System.Xml.Linq;

namespace competex_backend.API.DTOs
{
    public class MemberDTO
    {
        public Guid MemberId { get; set; }
        public string Name { get; set; }
    }
}

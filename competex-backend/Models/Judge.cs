using competex_backend.Models;

namespace competex_backend.Models
{
    public class Judge : Identifiable
    {
        public JudgeType JudgeType { get; set; }
        public Member Member { get; set; }  

        public string Description { get; set; }

        public Judge()
        {
        }
    }
}

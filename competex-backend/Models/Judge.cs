namespace competex_backend.Models
{
    public class Judge : Identifiable
    {
        public JudgeType JudgeType { get; set; }
        public Guid MemberId { get; set; }  
        public string Description { get; set; }
    }
}

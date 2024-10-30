namespace competex_backend.Models
{
    public class Penalty
    {
        public Guid PenaltyId { get; set; }
        public PenaltyType PenaltyType { get; set; }    
        public object? PenaltyValue { get; set; }
    }
}

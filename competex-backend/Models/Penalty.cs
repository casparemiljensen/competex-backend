namespace competex_backend.Models
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public PenaltyType PenaltyType { get; set; }    
        public object? PenaltyValue { get; set; }
    }
}

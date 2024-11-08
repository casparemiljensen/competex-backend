namespace competex_backend.Models
{
    public class Penalty : IIdentifiable
    {
        public Guid Id { get; set; }
        public PenaltyType PenaltyType { get; set; }    
        public object? PenaltyValue { get; set; }
    }
}

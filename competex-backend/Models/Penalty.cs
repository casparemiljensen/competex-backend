namespace competex_backend.Models
{
    public class Penalty : IIdentifiable
    {
        public Guid Id { get; init; }
        public PenaltyType PenaltyType { get; set; }    
        public object? PenaltyValue { get; set; }
    }
}

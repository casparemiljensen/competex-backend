namespace competex_backend.Models
{
    public class Penalty : Identifiable
    {
        public PenaltyType PenaltyType { get; set; }
        public object? PenaltyValue { get; set; }
    }
}

using System;

namespace competex_backend.Models;

public class Round : Identifiable
{
    public required string Name { get; set; }
    public uint SequenceNumber { get; set; }
    public RoundType RoundType { get; set; }
    public Guid CompetitionId { get; init; }
    public RoundStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    //public IEnumerable<Guid> MatchIds { get; set; } = new List<Guid>();


    public override string ToString()
    {
        return $"Id: {Id}\nName: {Name}\nSequenceNumber: {SequenceNumber}";
    }
}
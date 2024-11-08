using System;

namespace competex_backend.Models;

public class Round : IIdentifiable
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public uint SequenceNumber { get; set; }
    public RoundType RoundType { get; set; }
    public Guid CompetitionId { get; init; }
    public RoundStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public IEnumerable<Guid> Matches { get; set; }


    public override string ToString()
    {
        return $"Id: {Id}\nName: {Name}\nSequenceNumber: {SequenceNumber}";
    }
}
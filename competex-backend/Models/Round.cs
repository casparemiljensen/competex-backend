using System;

namespace competex_backend.Models;

public class Round : IIdentifiable
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public uint SequenceNumber { get; set; }
    public RoundType RoundType { get; set; }
    public Guid? CompetitionId { get; init; }
    public RoundStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid[] Matches { get; set; }

    public Round(Guid id, string name, uint sequenceNumber, RoundType roundType, Guid competitionId, RoundStatus status, DateTime startTime, DateTime endTime, Guid[] matches)
    {
        Id = id;
        Name = name.Trim();
        SequenceNumber = sequenceNumber;
        RoundType = roundType;
        CompetitionId = competitionId;
        Status = status;
        StartTime = startTime;
        EndTime = endTime;
        Matches = matches;
    }

    public Round(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        SequenceNumber = 0;
        RoundType = RoundType.Base;
        CompetitionId = null;
        Status = RoundStatus.Future;
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
        Matches = [];
    }

    public override string ToString()
    {
        return $"Id: {Id}\nName: {Name}\nSequenceNumber: {SequenceNumber}";
    }
}
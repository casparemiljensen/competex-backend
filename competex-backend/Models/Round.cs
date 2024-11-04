using System;

namespace competex_backend.Models;

public class Round : IIdentifiable
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public uint SequenceNumber { get; set; }
    public required RoundTypeEnum RoundType { get; set; }
    public Guid CompetitionId { get; init; }
    public RoundStatusEnum Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required Guid[] Matches { get; set; }
}

public enum RoundTypeEnum
{
    roundRobin,
    Tournament
}

public enum RoundStatusEnum
{
    Future,
    Starting,
    Ongoing,
    Ended
}
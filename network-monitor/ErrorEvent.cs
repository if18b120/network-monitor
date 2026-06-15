using System.Net.NetworkInformation;

public class ErrorEvent
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public IPStatus Status { get; init; }
}
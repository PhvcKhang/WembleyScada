namespace WembleyScada.Host.Application.Commands.ExecutionTime;

public class ExecutionTimeNotification : INotification
{
    public string StationId { get; set; }
    public double ExecutionTime { get; set; }

    public ExecutionTimeNotification(string stationId, double executionTime)
    {
        StationId = stationId;
        ExecutionTime = executionTime;
    }
}

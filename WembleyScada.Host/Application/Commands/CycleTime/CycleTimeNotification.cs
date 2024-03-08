namespace WembleyScada.Host.Application.Commands.CycleTime;
public class CycleTimeNotification : INotification
{
    public string LineId { get; set; }
    public string StationId { get; set; }
    public double CycleTime { get; set; }
    public DateTime Timestamp { get; set; }

    public CycleTimeNotification(string lineId, string stationId, double cycleTime, DateTime timestamp)
    {
        LineId = lineId;
        StationId = stationId;
        CycleTime = cycleTime;
        Timestamp = timestamp;
    }
}

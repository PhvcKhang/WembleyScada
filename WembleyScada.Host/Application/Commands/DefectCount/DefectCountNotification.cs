namespace WembleyScada.Host.Application.Commands.DefectCount;

public class DefectCountNotification : INotification
{
    public string LineId { get; set; }
    public string StationId { get; set; }
    public int DefectCount { get; set; }
    public DateTime Timestamp { get; set; }

    public DefectCountNotification(string lineId, string stationId, int defectCount, DateTime timestamp)
    {
        LineId = lineId;
        StationId = stationId;
        DefectCount = defectCount;
        Timestamp = timestamp;
    }
}

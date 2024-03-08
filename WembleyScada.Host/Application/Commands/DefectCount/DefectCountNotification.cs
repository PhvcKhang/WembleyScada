namespace WembleyScada.Host.Application.Commands.DefectCount;

public class DefectCountNotification : INotification
{
    public string LineId { get; set; }
    public string DeviceId { get; set; }
    public int DefectCount { get; set; }
    public DateTime Timestamp { get; set; }

    public DefectCountNotification(string lineId, string deviceId, int defectCount, DateTime timestamp)
    {
        LineId = lineId;
        DeviceId = deviceId;
        DefectCount = defectCount;
        Timestamp = timestamp;
    }
}

namespace WembleyScada.Host.Application.Commands.ErrorStatus;

public class ErrorStatusNotification : INotification
{
    public string LineId { get; set; }
    public string DeviceId { get; set; }
    public string ErrorId { get; set; }
    public int Value { get; set; }
    public DateTime Timestamp { get; set; }

    public ErrorStatusNotification(string lineId, string deviceId, string errorId, int value, DateTime timestamp)
    {
        LineId = lineId;
        DeviceId = deviceId;
        ErrorId = errorId;
        Value = value;
        Timestamp = timestamp;
    }
}

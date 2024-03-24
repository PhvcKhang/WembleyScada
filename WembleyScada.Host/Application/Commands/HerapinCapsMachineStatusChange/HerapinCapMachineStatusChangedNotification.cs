namespace WembleyScada.Host.Application.Commands.HerapinCapsMachineStatusChange;

public class HerapinCapMachineStatusChangedNotification : INotification
{
    public string LineId { get; set; }
    public string StationId { get; set; }
    public EMachineStatus MachineStatus { get; set; }
    public DateTime Timestamp { get; set; }
    public HerapinCapMachineStatusChangedNotification(string lineId, string stationId, EMachineStatus machineStatus, DateTime timestamp)
    {
        LineId = lineId;
        StationId = stationId;
        MachineStatus = machineStatus;
        Timestamp = timestamp;
    }
}

namespace WembleyScada.Host.Application.Commands.HerapinCapsMachineStatusChange;

public class HerapinCapMachineStatusChangedNotification : INotification
{
    public string StationId { get; set; }
    public string LineId { get; set; }
    public EMachineStatus MachineStatus { get; set; }
    public DateTime Timestamp { get; set; }

    public HerapinCapMachineStatusChangedNotification(string stationId, string lineId, EMachineStatus machineStatus, DateTime timestamp)
    {
        StationId = stationId;
        LineId = lineId;
        MachineStatus = machineStatus;
        Timestamp = timestamp;
    }
}

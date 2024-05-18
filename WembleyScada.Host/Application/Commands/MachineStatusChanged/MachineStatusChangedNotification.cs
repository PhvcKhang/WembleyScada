namespace WembleyScada.Host.Application.Commands.MachineStatusChanged;
public class MachineStatusChangedNotification : INotification
{
    public string LineId { get; set; }
    public string StationId { get; set; }
    public EMachineStatus MachineStatus { get; set; }
    public DateTime Timestamp { get; set; }

    public MachineStatusChangedNotification(string lineId, string stationId, EMachineStatus machineStatus, DateTime timestamp)
    {
        LineId = lineId;
        StationId = stationId;
        MachineStatus = machineStatus;
        Timestamp = timestamp;
    }
}

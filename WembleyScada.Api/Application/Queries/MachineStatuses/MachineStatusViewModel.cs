namespace WembleyScada.Api.Application.Queries.MachineStatuses;

public class MachineStatusViewModel
{
    public string StationId { get; set; }
    public int ShiftNumber { get; set; }
    public EMachineStatus Status { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; set; }

    public MachineStatusViewModel(string stationId, int shiftNumber, EMachineStatus status, DateTime date, DateTime timestamp)
    {
        StationId = stationId;
        Status = status;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }
}

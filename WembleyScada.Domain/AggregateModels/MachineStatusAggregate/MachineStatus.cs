
namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public class MachineStatus : IAggregateRoot
{
    public string MachineStatusId { get; private set; }
    public int ShiftNumber { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime Timestamp { get; private set; }
    public EMachineStatus Status { get; private set; }
    public string StationId { get; private set; }
    public Station Station { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MachineStatus() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MachineStatus(string machineStatusId, string stationId, Station station, int shiftNumber, DateTime date, EMachineStatus status, DateTime timestamp)
    {
        MachineStatusId = machineStatusId;
        StationId = stationId;
        Station = station;
        ShiftNumber = shiftNumber;
        Date = date;
        Status = status;
        Timestamp = timestamp;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MachineStatus(Station station, EMachineStatus status, DateTime timestamp)
    {
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);
        var date = ShiftTimeHelper.GetShiftDate(timestamp);

        Station = station;
        ShiftNumber = shiftNumber;
        Date = date;
        Status = status;
        Timestamp = timestamp;
    }

    public MachineStatus(Station station, EMachineStatus status, int shiftNumber, DateTime date, DateTime timestamp)
    {
        Station = station;
        Status = status;
        ShiftNumber = shiftNumber;
        Date = date;
        Timestamp = timestamp;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

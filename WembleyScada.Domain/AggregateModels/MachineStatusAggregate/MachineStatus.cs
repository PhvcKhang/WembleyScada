
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
}

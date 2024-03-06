namespace WembleyScada.Api.Application.Queries.MachineStatuses;

public class MachineStatusesQuery: IRequest<IEnumerable<MachineStatusViewModel>>
{
    public string? StationId { get; }
    public DateTime StartTime { get; } = DateTime.MinValue;
    public DateTime EndTime { get; } = DateTime.Now;
}

namespace WembleyScada.Api.Application.Queries.MachineStatuses;

public class MachineStatusesQuery: IRequest<IEnumerable<MachineStatusViewModel>>
{
    public string? StationId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}

namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportsQuery: PaginatedQuery, IRequest<IEnumerable<ShiftReportViewModel>>
{
    public string? StationId { get; }
    public DateTime StartTime { get; } = DateTime.MinValue;
    public DateTime EndTime { get; } = DateTime.Now;
}

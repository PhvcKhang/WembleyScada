namespace WembleyScada.Api.Application.Queries.ShiftReports.Lastest;

public class ShiftReportLatestDetailsQuery : IRequest<IEnumerable<ShotOEEViewModel>>
{
    public string? StationId { get; }
    public int Interval { get; }
}

namespace WembleyScada.Api.Application.Queries.ShiftReports.Lastest;

public class ShiftReportLatestDetailsQuery : IRequest<IEnumerable<ShotOEEViewModel>>
{
    public string? StationId { get; set; }
    public int Interval { get; set; }
}

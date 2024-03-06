namespace WembleyScada.Api.Application.Queries.ShiftReports.ShortenDetail;

public class ShiftReportShortenDetailsQuery : IRequest<IEnumerable<ShiftReportDetailViewModel>>
{
    public string? ShiftReportId { get; set; }
    public string? StationId { get; set; }
    public DateTime? Date { get; set; }
    public int? ShiftNumber { get; set; }
    public int Interval { get; set; }
}

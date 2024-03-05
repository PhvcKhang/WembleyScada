namespace WembleyScada.Api.Application.Queries.ShiftReports.ShortenDetail
{
    public class ShiftReportShortenDetailsQuery : IRequest<IEnumerable<ShiftReportDetailViewModel>>
    {
        public string? ShiftReportId { get; }
        public string? StationId { get; }
        public DateTime? Date { get; }
        public int? ShiftNumber { get; }
        public int Interval { get; }
    }
}

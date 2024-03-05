namespace WembleyScada.Api.Application.Queries.ShiftReports.Details
{
    public class ShiftReportDetailsQuery: PaginatedQuery, IRequest<IEnumerable<ShiftReportDetailViewModel>>
    {
        public string? ShiftReportId { get; set; }
        public string? StationId { get; set; }
        public DateTime? Date { get; set; }
        public int? ShiftNumber { get; set; }
    }
}

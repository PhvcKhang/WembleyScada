namespace WembleyScada.Api.Application.Queries.ShiftReports.Download;

public class DownloadReportsQuery : IRequest<byte[]>
{
    public string? StationId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.MinValue;
    public DateTime EndTime { get; set; } = DateTime.Now;
}

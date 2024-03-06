namespace WembleyScada.Api.Application.Queries.ShiftReports.Download;

public class DownloadReportsQuery : IRequest<byte[]>
{
    public string? StationId { get; }
    public DateTime StartTime { get; } = DateTime.MinValue;
    public DateTime EndTime { get; } = DateTime.Now;
}

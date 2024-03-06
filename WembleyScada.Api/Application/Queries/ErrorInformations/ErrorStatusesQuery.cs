namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusesQuery : IRequest<IEnumerable<ErrorStatusViewModel>>
{
    public string? StationId { get; }
    public DateTime StartTime { get; } = DateTime.MinValue;
    public DateTime EndTime { get;  } = DateTime.Now;
}

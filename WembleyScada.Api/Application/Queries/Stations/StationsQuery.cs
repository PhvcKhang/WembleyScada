namespace WembleyScada.Api.Application.Queries.Stations;

public class StationsQuery : IRequest<IEnumerable<StationViewModel>>
{ 
    public string? LineId { get; set; }
}

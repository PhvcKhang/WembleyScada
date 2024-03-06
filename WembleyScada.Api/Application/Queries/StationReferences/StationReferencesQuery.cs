namespace WembleyScada.Api.Application.Queries.StationReferences;

public class StationReferencesQuery : IRequest<IEnumerable<StationReferenceViewModel>>
{
    public string? StationId { get; set; }
    public string? ReferenceId { get; set; }
}

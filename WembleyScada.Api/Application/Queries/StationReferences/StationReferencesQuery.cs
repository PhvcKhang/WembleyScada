namespace WembleyScada.Api.Application.Queries.StationReferences;

public class StationReferencesQuery : IRequest<IEnumerable<StationReferenceViewModel>>
{
    public string? StationId { get; }
    public string? ReferenceId { get; }
}

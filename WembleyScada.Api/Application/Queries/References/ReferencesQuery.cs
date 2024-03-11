namespace WembleyScada.Api.Application.Queries.References;

public class ReferencesQuery : IRequest<IEnumerable<ReferenceViewModel>>
{
    public string? ProductId { get; set; }
    public string? LineId { get; set; }
}

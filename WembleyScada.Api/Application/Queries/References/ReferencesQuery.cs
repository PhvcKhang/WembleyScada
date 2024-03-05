namespace WembleyScada.Api.Application.Queries.References
{
    public class ReferencesQuery : IRequest<IEnumerable<ReferenceViewModel>>
    {
        public string? ProductId { get; }
        public ELineType? LineType { get; }
    }
}

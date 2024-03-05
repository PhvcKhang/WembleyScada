namespace WembleyScada.Api.Application.Queries.References.Parameters
{
    public class ParametersQuery : IRequest<IEnumerable<ParameterViewModel>>
    {
        public string? ReferenceId { get; }
        public ELineType? LineType { get; }
    }
}

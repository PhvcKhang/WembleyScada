namespace WembleyScada.Api.Application.Queries.Lines;

public class LinesQuery : IRequest<IEnumerable<LineViewModel>>
{
    public ELineType? LineType { get; set; }
}

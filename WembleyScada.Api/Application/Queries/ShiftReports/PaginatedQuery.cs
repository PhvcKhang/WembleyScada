namespace WembleyScada.Api.Application.Queries;
public class PaginatedQuery
{
    public int PageIndex { get; } = 1;
    public int PageSize { get; } = 10000;
}

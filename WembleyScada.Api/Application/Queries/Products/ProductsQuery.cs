namespace WembleyScada.Api.Application.Queries.Products;

public class ProductsQuery : IRequest<IEnumerable<ProductViewModel>>
{
    public ELineType? LineType { get; set; }
}

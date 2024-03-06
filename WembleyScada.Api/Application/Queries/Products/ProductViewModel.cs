namespace WembleyScada.Api.Application.Queries.Products;

public class ProductViewModel
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public ProductViewModel(string productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

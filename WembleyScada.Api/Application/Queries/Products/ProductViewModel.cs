namespace WembleyScada.Api.Application.Queries.Products;

public class ProductViewModel
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ProductViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ProductViewModel(string productId, string productName)
    {
        ProductId = productId;
        ProductName = productName;
    }
}

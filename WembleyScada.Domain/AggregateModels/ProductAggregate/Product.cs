namespace WembleyScada.Domain.AggregateModels.ProductAggregate;

public class Product : IAggregateRoot
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set;}
    public List<Line> UsableLines { get; private set; }
    public List<Station> Stations {  get; private set; }
    public List<Reference> References { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

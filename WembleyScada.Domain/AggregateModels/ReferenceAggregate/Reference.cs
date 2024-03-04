

namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate
{
    public class Reference : IAggregateRoot
    {
        public string ReferenceId { get; private set; }
        public string ReferenceName { get; private set; }
        public string ProductId { get; private set; }
        public Product Product { get; private set; }
        public List<Line> UsableLines { get; private set; }
        public List<Lot> Lots { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Reference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

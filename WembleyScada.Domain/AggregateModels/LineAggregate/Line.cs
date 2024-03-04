namespace WembleyScada.Domain.AggregateModels.LineAggregate
{
    public class Line : IAggregateRoot
    {
        public string LineId { get; private set; }
        public string LineName { get; private set; }
        public ELineType LineType { get; private set; }
        public List<Station> Stations { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Line() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}


namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

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
    public void AddLot(string lotCode, int lotSize, ELotStatus lotStatus, DateTime startTime)
    {
        var lot = new Lot(lotCode, lotSize, lotStatus, startTime);

        if (Lots.Any(d => d.LotCode == lotCode))
        {
            throw new ChildEntityDuplicationException(lotCode, lot, ReferenceId, this);
        }

        Lots.Add(lot);
    }
    public void UpdateLot(string lotId, int lotSize)
    {
        var lot = Lots.FirstOrDefault(x => x.LotStatus == ELotStatus.Working);

        lot?.Update(lotId, lotSize);
    }
    public void UpdateLotStatus(ELotStatus lotStatus, DateTime endTime)
    {
        var lot = Lots.Find(x => x.LotStatus == ELotStatus.Working);
        if (lot is null)
        {
            throw new Exception($"All Lots is completed in this RefName: {ReferenceName}");
        }
        lot.UpdateStatus(lotStatus, endTime);
    }
}

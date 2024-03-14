namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public class Lot
{
    public string LotId { get; private set; }
    public string LotCode { get; private set; }
    public int LotSize { get; private set; }
    public string ReferenceId { get; private set; }
    public ELotStatus LotStatus { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Lot() { }
    public Lot(string lotCode, int lotSize, ELotStatus lotStatus, DateTime startTime, string referenceId)
    {
        LotCode = lotCode;
        LotSize = lotSize;
        LotStatus = lotStatus;
        StartTime = startTime;
        ReferenceId = referenceId;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public void Update(string lotCode, int lotSize)
    {
        LotCode = lotCode;
        LotSize = lotSize;
    }

    public void UpdateStatus(ELotStatus lotStatus, DateTime endTime)
    {
        LotStatus = lotStatus;
        EndTime = endTime;
    }
}

namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public class ErrorStatus
{
    public string ErrorStatusId { get; private set; }
    public int Value { get; private set; }
    public int ShiftNumber { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string ErrorId { get; private set; }
    public ErrorInformation ErrorInformation { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ErrorStatus() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

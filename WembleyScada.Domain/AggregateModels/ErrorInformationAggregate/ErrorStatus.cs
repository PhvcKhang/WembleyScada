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
    public ErrorStatus(int value, DateTime timestamp)
    {
        var date = ShiftTimeHelper.GetShiftDate(timestamp);
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(timestamp);

        Value = value;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }

    public ErrorStatus(int value, DateTime date, int shiftNumber, DateTime timestamp)
    {
        Value = value;
        Date = date;
        ShiftNumber = shiftNumber;
        Timestamp = timestamp;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}

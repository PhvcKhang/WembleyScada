namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusViewModel
{
    public string ErrorId { get; set; }
    public string ErrorName { get; set; }
    public int ShiftNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime Timestamp { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ErrorStatusViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ErrorStatusViewModel(string errorId, string errorName, int shiftNumber, DateTime date, DateTime timestamp)
    {
        ErrorId = errorId;
        ErrorName = errorName;
        ShiftNumber = shiftNumber;
        Date = date;
        Timestamp = timestamp;
    }
}

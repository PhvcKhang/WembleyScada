﻿namespace WembleyScada.Domain.AggregateModels.ErrorInformationAggregate;

public class ErrorInformation : IAggregateRoot
{
    public string ErrorId { get; private set; }
    public string ErrorName { get; private set; }
    public string StationId { get; private set; }
    public Station Station { get; private set; }
    public List<ErrorStatus> ErrorStatuses { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ErrorInformation() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public void AddErrorStatus(int value, DateTime date, int shiftNumber, DateTime timestamp)
    {
        var errorStatus = new ErrorStatus(value, date, shiftNumber, timestamp);
        ErrorStatuses.Add(errorStatus);
    }
}

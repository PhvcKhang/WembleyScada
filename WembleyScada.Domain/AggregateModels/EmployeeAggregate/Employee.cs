namespace WembleyScada.Domain.AggregateModels.EmployeeAggregate;

public class Employee : IAggregateRoot
{
    public string EmployeeId { get; private set; }
    public string EmployeeName { get; private set; }
    public List<WorkRecord> WorkRecords { get; private set; } = new List<WorkRecord>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Employee() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Employee(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
    public void AddWorkRecord(Station station, EWorkStatus workStatus, DateTime startTime)
    {
        if (WorkRecords.Any(x => x.WorkStatus == EWorkStatus.Working && x.StationId == station.StationId))
        {
            throw new Exception($"The entity PersonWorkRecord already existed with the same StationId:{station.StationId}");
        }
        var personWorkRecord = new WorkRecord(station, startTime, workStatus);
        WorkRecords.Add(personWorkRecord);
    }

    public void DeleteWorkRecords()
    {
        WorkRecords.RemoveAll(x => x.WorkStatus == EWorkStatus.Working);
    }
}

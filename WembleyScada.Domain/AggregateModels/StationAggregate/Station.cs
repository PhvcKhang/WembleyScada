using WembleyScada.Domain.AggregateModels.EmployeeAggregate;

namespace WembleyScada.Domain.AggregateModels.StationAggregate;

public class Station : IAggregateRoot
{
    public string StationId { get; private set; }
    public string StationName { get; private set; }
    public string LineId { get; private set; }
    public Line Line { get; private set; }
    public List<ShiftReport> ShiftReports { get; private set; }
    public List<WorkRecord> EmployeeWorkRecords { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Station() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public void DeleteWorkRecords(string employeeId)
    {
        var workRecords = EmployeeWorkRecords
            .Where(x => x.WorkStatus == EWorkStatus.Working)
            .ToList();

        var workRecord = workRecords.Find(x => x.EmployeeId == employeeId)
            ?? throw new Exception($"Employee {employeeId} is not working on this Station {StationId}");

        EmployeeWorkRecords.Remove(workRecord);
    }
}

namespace WembleyScada.Api.Application.Queries.Employees;

public class WorkRecordViewModel
{
    public StationViewModel Station { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public WorkRecordViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public WorkRecordViewModel(StationViewModel station, DateTime startTime, DateTime endTime)
    {
        Station = station;
        StartTime = startTime;
        EndTime = endTime;
    }
}

namespace WembleyScada.Api.Application.Queries.Employees
{
    public class WorkRecordViewModel
    {
        public StationViewModel Station { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public WorkRecordViewModel(StationViewModel station, DateTime startTime, DateTime endTime)
        {
            Station = station;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}

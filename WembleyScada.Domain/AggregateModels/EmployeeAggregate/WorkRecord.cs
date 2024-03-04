namespace WembleyScada.Domain.AggregateModels.EmployeeAggregate
{
    public class WorkRecord
    {
        public string WorkRecordId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set;}
        public EWorkStatus WorkStatus { get; private set; }
        public string StationId { get; private set; }
        public string EmployeeId { get; private set; }
        public Station Station { get; private set; }
        public Employee Employee { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public WorkRecord() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}

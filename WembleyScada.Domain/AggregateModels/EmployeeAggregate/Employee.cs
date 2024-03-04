namespace WembleyScada.Domain.AggregateModels.EmployeeAggregate
{
    public class Employee : IAggregateRoot
    {
        public string EmployeeId { get; private set; }
        public string EmployeeName { get; private set; }
        public List<WorkRecord> WorkRecords { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Employee() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}

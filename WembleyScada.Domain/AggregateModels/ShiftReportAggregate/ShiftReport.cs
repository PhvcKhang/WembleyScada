namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate
{
    public class ShiftReport: IAggregateRoot
    {
        public string ShiftReportId { get; private set; }
        public int ProductCount { get; private set; }
        public int DefectCount { get; private set; }
        public int ShiftNumber { get; private set; }
        public DateTime Date { get; set; }
        public TimeSpan ElapsedTime {  get; private set; }
        public string StationId { get; private set; }
        public Station Station { get; private set; }
        public List<Shot> Shots {  get; private set; }

        public double A;
        public double P;
        public double Q => Shots.Count > 0 ? (double)(ProductCount - DefectCount) / (double)ProductCount : 0;
        public double OEE => Shots.Count > 0 ? A * P * Q : 0;
        public double TotalExecutionTime => Shots.Sum(x => x.ExecutionTime);
        public double TotalCycleTime => Shots.Sum(x => x.CycleTime);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ShiftReport() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}

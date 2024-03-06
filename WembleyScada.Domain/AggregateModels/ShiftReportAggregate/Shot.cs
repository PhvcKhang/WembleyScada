namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

public class Shot
{
    public double ExecutionTime { get; private set; }
    public double CycleTime { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public double A { get; private set; }
    public double P { get; private set; }
    public double Q { get; private set; }
    public double OEE { get; private set; }

}

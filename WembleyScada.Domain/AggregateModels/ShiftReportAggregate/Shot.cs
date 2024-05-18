namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

public class Shot
{
    public double CycleTime { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public double A { get; private set; }
    public double P { get; private set; }
    public double Q { get; private set; }
    public double OEE { get; private set; }

    public Shot(double cycleTime, DateTime timeStamp, double a, double p, double q, double oEE)
    {
        CycleTime = cycleTime;
        TimeStamp = timeStamp;
        A = a;
        P = p;
        Q = q;
        OEE = oEE;
    }
    public Shot(DateTime timeStamp, double a, double p, double q, double oEE)
    {
        CycleTime = 0;
        TimeStamp = timeStamp;
        A = a;
        P = p;
        Q = q;
        OEE = oEE;
    }
}

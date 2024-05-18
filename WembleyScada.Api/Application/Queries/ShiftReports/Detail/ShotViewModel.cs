namespace WembleyScada.Api.Application.Queries.ShiftReports.Details;

public class ShotViewModel
{
    public double ExecutionTime { get; set; }
    public double CycleTime { get; set; }
    public DateTime TimeStamp { get; set; }
    public double A { get; set; }
    public double P { get; set; }
    public double Q { get; set; }
    public double OEE { get; set; }

    public ShotViewModel()
    {
    }

    public ShotViewModel(double executionTime, double cycleTime, DateTime timeStamp, double a, double p, double q, double oEE)
    {
        ExecutionTime = executionTime;
        CycleTime = cycleTime;
        TimeStamp = timeStamp;
        A = a;
        P = p;
        Q = q;
        OEE = oEE;
    }
}

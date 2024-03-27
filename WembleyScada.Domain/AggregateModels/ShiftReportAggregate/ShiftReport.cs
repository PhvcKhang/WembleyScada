namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
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
    public List<Shot> Shots {  get; set; }

    public double A { get; private set; }
    public double P { get; private set; }
    public double Q => Shots.Count > 0 ? (double)(ProductCount - DefectCount) / (double)ProductCount : 0;
    public double OEE => Shots.Count > 0 ? A * P * Q : 0;
    public double TotalExecutionTime => Shots.Sum(x => x.ExecutionTime);
    public double TotalCycleTime => Shots.Sum(x => x.CycleTime);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ShiftReport() { }
    public ShiftReport(int shiftNumber, DateTime date, Station station)
    {
        ShiftNumber = shiftNumber;
        Date = date;
        Station = station;
    }
    public ShiftReport(Station station, DateTime time)
    {
        var shiftNumber = ShiftTimeHelper.GetShiftNumber(time);
        var date = ShiftTimeHelper.GetShiftDate(time);

        Shots = new List<Shot>();

        ShiftNumber = shiftNumber;
        Date = date;
        Station = station;
        StationId = station.StationId;

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public void AddHerapinCapShot(DateTime timestamp, double a, double p, double q, double oEE)
    {
        if (!Shots.Any(x => x.TimeStamp == timestamp))
        {
            var shot = new Shot(timestamp, a, p, q, oEE);
            Shots.Add(shot);
        }
    }

    public void AddShot(double executionTime, double cycleTime, DateTime timestamp, double a, double p, double q, double oEE)
    {
        if (!Shots.Any(x => x.TimeStamp == timestamp))
        {
            var shot = new Shot(executionTime, cycleTime, timestamp, a, p, q, oEE);
            Shots.Add(shot);
        }
    }

    public void SetProductCount(int productCount) => ProductCount = productCount;

    public void SetDefectCount(int defectCount) => DefectCount = defectCount;

    public void SetElapsedTime(TimeSpan elapsedTime) => ElapsedTime = elapsedTime;

    public void SetA(double a) => A = a;

    public void SetP(double p) => P = p > 1 ? 1 : p;

}

namespace WembleyScada.Api.Application.Queries.ShiftReports.Details;

public class ShiftReportDetailViewModel
{
    public double OEE { get; set; }
    public double A { get; set; }
    public double P { get; set; }
    public double Q { get; set; }
    public DateTime Date { get; set; }
    public int ShiftNumber { get; set; }
    public string DeviceId { get; set; }
    public int ProductCount { get; set; }
    public int DefectCount { get; set; }
    public List<ShotViewModel> Shots { get; set; }

    public ShiftReportDetailViewModel(double oEE, double a, double p, double q, DateTime date, int shiftNumber, string deviceId, int productCount, int defectCount, List<ShotViewModel> shots)
    {
        OEE = oEE;
        A = a;
        P = p;
        Q = q;
        Date = date;
        ShiftNumber = shiftNumber;
        DeviceId = deviceId;
        ProductCount = productCount;
        DefectCount = defectCount;
        Shots = shots;
    }
}

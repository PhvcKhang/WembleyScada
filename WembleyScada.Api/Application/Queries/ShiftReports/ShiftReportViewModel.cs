namespace WembleyScada.Api.Application.Queries.ShiftReports
{
    public class ShiftReportViewModel
    {
        public string ShiftReportId { get; set; }
        public double OEE { get; set; }
        public double A { get; set; }
        public double P { get; set; }
        public double Q { get; set; }
        public DateTime Date { get; set; }
        public int ShiftNumber { get; set; }
        public string StationId { get; set; }
        public int ProductCount { get; set; }
        public int DefectCount { get; set; }

        public ShiftReportViewModel(string shiftReportId, double oEE, double a, double p, double q, DateTime date, int shiftNumber, string stationId, int productCount, int defectCount)
        {
            ShiftReportId = shiftReportId;
            OEE = oEE;
            A = a;
            P = p;
            Q = q;
            Date = date;
            ShiftNumber = shiftNumber;
            StationId = stationId;
            ProductCount = productCount;
            DefectCount = defectCount;
        }
    }
}

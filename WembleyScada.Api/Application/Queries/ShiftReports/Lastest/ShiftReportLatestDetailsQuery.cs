namespace WembleyScada.Api.Application.Queries.ShiftReports.Lastest;

public class ShiftReportLatestDetailsQuery : IRequest<IEnumerable<ShotOEEViewModel>>
{
    public string? StationId { get; set; }
    public int Interval { get; set; } 
    //Trang vẽ đồ thị OEE => Khi reload, trang giao diện bị mất, gọi tới latest để vẽ lại đồ thị của ca đó
    // Interval => khoảng cách giữa các step
}

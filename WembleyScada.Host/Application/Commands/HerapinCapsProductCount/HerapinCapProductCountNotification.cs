namespace WembleyScada.Host.Application.Commands.HerapinCapsProductCount;

public class HerapinCapProductCountNotification : INotification
{
    public string LineId { get; set; }
    public string StationId { get; set; }
    public int ProductCount { get; set; }
    public DateTime Timestamp { get; set; }

    public HerapinCapProductCountNotification(string lineId, string stationId, int productCount, DateTime timestamp)
    {
        LineId = lineId;
        StationId = stationId;
        ProductCount = productCount;
        Timestamp = timestamp;
    }
}

namespace WembleyScada.Host.Application.Buffers;

public class StatusTimeBuffers
{
    private readonly Dictionary<string, DateTime> startTimes = new();
    private readonly Dictionary<string, DateTime> startRunningTimes = new();
    private readonly Dictionary<string, TimeSpan> totalPreviousRunningTimes = new();

    public void UpdateStartTime(string stationId, DateTime startTime)
        => startTimes[stationId] = startTime;

    public DateTime GetStartTime(string stationId)
        => startTimes[stationId];

    public void UpdateStartRunningTime(string stationId, DateTime startRunningTime)
        => startRunningTimes[stationId] = startRunningTime;

    public DateTime GetStartRunningTime(string stationId)
        => startRunningTimes[stationId];

    public void UpdateTotalPreviousRunningTime(string stationId, TimeSpan totalPreviousRunningTime)
        => totalPreviousRunningTimes[stationId] = totalPreviousRunningTime;

    public TimeSpan GetTotalPreviousRunningTime(string stationId)
        => totalPreviousRunningTimes[stationId];
}

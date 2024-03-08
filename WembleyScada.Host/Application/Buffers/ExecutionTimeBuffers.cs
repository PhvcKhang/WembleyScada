namespace WembleyScada.Host.Application.Buffers;

public class ExecutionTimeBuffers
{
    private readonly Dictionary<string, double> stationsLastestExecutionTimes = new();

    public void Update(string stationId, double executionTime) 
        => stationsLastestExecutionTimes[stationId] = executionTime;
    public double GetStationLatestExecutionTime(string stationId) 
        => stationsLastestExecutionTimes[stationId];
}

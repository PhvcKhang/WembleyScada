namespace WembleyScada.Host.Application.Services;

public class MetricMessagePublisher
{
    private readonly ManagedMqttClient _mqttClient;

    public MetricMessagePublisher(ManagedMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public async Task PublishMetricMessage(string lineId, string stationId, string metricName, object value, DateTime timestamp)
    {
        var topic = $"{lineId}/{stationId}/Metric/{metricName}";

        var metricMessage = new MetricMessage(metricName, value, timestamp);

        var json = JsonConvert.SerializeObject(new List<MetricMessage>() { metricMessage });

        await _mqttClient.Publish(topic, json, true);
    }
}

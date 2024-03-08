namespace WembleyScada.Host.Application.Services;

public class MetricMessagePublisher
{
    private readonly ManagedMqttClient _mqttClient;

    public MetricMessagePublisher(ManagedMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public async Task PublishMetricMessage(string deviceType, string deviceId, string metricName, object value, DateTime timestamp)
    {
        var topic = $"{deviceType}/{deviceId}/Metric/{metricName}";

        var metricMessage = new MetricMessage(metricName, value, timestamp);

        var json = JsonConvert.SerializeObject(new List<MetricMessage>() { metricMessage });

        await _mqttClient.Publish(topic, json, true);
    }
}

namespace WembleyScada.Api.Application.Hubs;

public class ScadaHost : BackgroundService
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly Buffer _buffer;
    private readonly IHubContext<NotificationHub> _hubContext;

    public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer, IHubContext<NotificationHub> hubContext)
    {
        _mqttClient = mqttClient;
        _buffer = buffer;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectToMqttBrokerAsync();
    }

    private async Task ConnectToMqttBrokerAsync()
    {
        _mqttClient.MessageReceived += OnMqttClientMessageReceived;
        await _mqttClient.ConnectAsync();

        await _mqttClient.Subscribe("HCM/+/Metric");
        await _mqttClient.Subscribe("HCM/+/Metric/+");
    }

    private async Task OnMqttClientMessageReceived(MqttMessage arg)
    {
        var topic = arg.Topic;
        var payloadMessage = arg.Payload;

        if(topic is null || payloadMessage is null)
        {
            return;
        }

        var topicSegments = topic.Split('/');
        var lineId = topicSegments[0];
        var stationId = topicSegments[1];

        var metrics = JsonConvert.DeserializeObject<List<MetricMessage>>(payloadMessage);
        
        if(metrics is null)
        {
            return;
        }
        
        foreach(var metric in metrics)
        {
            var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
            await _buffer.Update(notification);
            string json = JsonConvert.SerializeObject(notification);
            await _hubContext.Clients.All.SendAsync("TagChanged", json);
        }
    }
}

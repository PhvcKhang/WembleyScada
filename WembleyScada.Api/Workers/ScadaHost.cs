namespace WembleyScada.Api.Workers;

public class ScadaHost : BackgroundService
{
    #region Properties
    private readonly ManagedMqttClient _mqttClient;
    private readonly Buffer _buffer;
    private readonly HubUserStorage _hubUserStorage;
    private readonly IHubContext<NotificationHub> _hubContext;
    #endregion

    #region Constructor & MQTT-related methods
    public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer, HubUserStorage hubUserStorage ,IHubContext<NotificationHub> hubContext)
    {
        _mqttClient = mqttClient;
        _buffer = buffer;
        _hubUserStorage = hubUserStorage;
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

        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+/+");
        await _mqttClient.Subscribe("HCM/IE-F2-HCA01/Metric/+");
    }
    #endregion

    #region On Receiving Messages
    private async Task OnMqttClientMessageReceived(MqttMessage mqttMessage)
    {       
        var payloadMessage = mqttMessage.Payload;

        var topic = mqttMessage.Topic;
        if (topic is null || payloadMessage is null) return;

        var metrics = JsonConvert.DeserializeObject<List<MetricMessage>>(payloadMessage);
        if (metrics is null) return;

        var topicSegments = topic.Split('/');

        //Desktop
        if (topicSegments[0] == "HCM")
        {
            await OnTagChangedNotificationReceived("HerapinCap", "IE-F2-HCA01", metrics);
            Console.WriteLine(payloadMessage);
        }

        //AR
        if (topicSegments[1] == "HerapinCap")
        {
            var lineId = topicSegments[0];
            var stationId = topicSegments[1];

            if (topicSegments[3] == "S1")
            {
                var users = await GetUsersSubToThisTopic(topic);

                if (topicSegments[4] == "in")
                {
                    await OnS1InputReceived(lineId, stationId, metrics);
                }
                else if (topicSegments[4] == "out")
                {
                    await OnS1OutputReceived(lineId, stationId, metrics, _hubUserStorage.Users);
                }
            }
            else if (topicSegments[3] == "S8")
            {

            }
        }
    }

    private async Task<List<HubUser>> GetUsersSubToThisTopic(string topic)
    {
        var users = _hubUserStorage.Users; 

        foreach (var user in users) 
        {
            var isContained = user.Topics.Any(userTopic => topic.Contains(userTopic));

            if(!isContained)
            {
                users.Remove(user);
            }
        }

        return await Task.FromResult(users);
    }
    #endregion

    #region HerapinCap Machine

    #region Send All Messages To Clients
    private async Task OnTagChangedNotificationReceived(string lineId, string stationId, List<MetricMessage> metrics)
    {
        foreach (var metric in metrics)
        {
            var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
            await _buffer.Update(notification);
            string json = JsonConvert.SerializeObject(notification);
            await _hubContext.Clients.All.SendAsync("TagChanged", json);
        }
    }
    #endregion

    #region Station 01
    private async Task OnS1OutputReceived(string lineId, string stationId, List<MetricMessage> metrics, List<HubUser> users)
    {
        //foreach (var user in users)
        //{
            foreach (var metric in metrics)
            {
                var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
                await _buffer.Update(notification);
                string json = JsonConvert.SerializeObject(notification);
                await _hubContext.Clients.All.SendAsync("S1Output", users.Count.ToString());
            }
        //}
    }

    private async Task OnS1InputReceived(string lineId, string stationId, List<MetricMessage> metrics)
    {
        //foreach (var user in users)
        //{
            foreach (var metric in metrics)
            {
                var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
                await _buffer.Update(notification);
                string json = JsonConvert.SerializeObject(notification);
                await _hubContext.Clients.All.SendAsync("S1Input", json);
            }
        //}
    }
    #endregion

    #region Station 8

    #endregion

    #endregion
}

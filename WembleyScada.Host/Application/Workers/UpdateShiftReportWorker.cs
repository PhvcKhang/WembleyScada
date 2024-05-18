namespace WembleyScada.Host.Application.Workers;

public class UpdateShiftReportWorker : BackgroundService
{
    private readonly ManagedMqttClient _mqttClient;
    private readonly IServiceScopeFactory _scopeFactory;

    public UpdateShiftReportWorker(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory)
    {
        _mqttClient = mqttClient;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConnectToMqttBrokerAsync();
    }

    private async Task ConnectToMqttBrokerAsync()
    {
        _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;

        await _mqttClient.ConnectAsync();

        //WembleyMedical/BTM/IE-F3-BLO06/Backend
        await _mqttClient.Subscribe("WembleyMedical/+/+/Backend/+");
        await _mqttClient.Subscribe("WembleyMedical/BTM/IE-F3-BLO06/Backend/+");
    }

    private async Task OnMqttClientMessageReceivedAsync(MqttMessage message)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var topic = message.Topic;
        var payloadMessage = message.Payload;
        if (topic is null || payloadMessage is null)
        {
            return;
        }

        var topicSegments = topic.Split('/');
        var lineId = topicSegments[1];
        var stationId = topicSegments[2];

        var metrics = JsonConvert.DeserializeObject<List<MetricMessage>>(payloadMessage);
        if (metrics is null)
        {
            return;
        }

        foreach (var metric in metrics)
        {
            var messageType = new MessageType(lineId, metric);

            switch (messageType.Value)
            {
                case MessageType.EMessageType.HerapinCapProductCount:
                    var herapinCapProductCount = Convert.ToInt32(metric.Value);
                    var herapinCapProductCountNotification = new HerapinCapProductCountNotification(lineId, stationId, herapinCapProductCount, metric.Timestamp);
                    await PublishNotification(herapinCapProductCountNotification, mediator);
                    break;
                case MessageType.EMessageType.HerapinCapMachineStatus:
                    var herapinCapStatusCode = (long)metric.Value;
                    var herapinCapMachineStatus = (EMachineStatus)herapinCapStatusCode;
                    var herapinCapMachineStatusNotification = new HerapinCapMachineStatusChangedNotification(lineId, stationId, herapinCapMachineStatus, metric.Timestamp);
                    await PublishNotification(herapinCapMachineStatusNotification, mediator);
                    break;

                case MessageType.EMessageType.MachineStatus:
                    var statusCode = (long)metric.Value;
                    var machineStatus = (EMachineStatus)statusCode;
                    var machineStatusNotification = new MachineStatusChangedNotification(lineId, stationId, machineStatus, metric.Timestamp);
                    await PublishNotification(machineStatusNotification, mediator);
                    break;
                case MessageType.EMessageType.CycleTime:
                    var cycleTime = Convert.ToDouble(metric.Value);
                    var cycleTimeNotification = new CycleTimeNotification(lineId, stationId, cycleTime, metric.Timestamp);
                    await PublishNotification(cycleTimeNotification, mediator);
                    break;
                case MessageType.EMessageType.DefectsCount:
                    var defectsCount = Convert.ToInt32(metric.Value);
                    var defectCountNotification = new DefectCountNotification(lineId, stationId, defectsCount, metric.Timestamp);
                    await PublishNotification(defectCountNotification, mediator);
                    break;
                case MessageType.EMessageType.ErrorStatus:
                    var errorValue = Convert.ToInt32(metric.Value);
                    var errorStatusNotification = new ErrorStatusNotification(lineId, stationId, metric.Name, errorValue, metric.Timestamp);
                    await PublishNotification(errorStatusNotification, mediator);
                    break;
            }
        }
    }
    private async Task PublishNotification(INotification notification, IMediator mediator)
    {
        try
        {
            await mediator.Publish(notification);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Exception: ");
            Console.WriteLine( ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

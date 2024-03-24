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

        await _mqttClient.Subscribe("HCM/IE-F2-HCA01/Metric");
        await _mqttClient.Subscribe("HCM/IE-F2-HCA01/Metric/+");
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
        var lineId = topicSegments[0];
        var stationId = topicSegments[1];

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
                    var productCount = Convert.ToInt32(metric.Value);
                    var herapinCapProductCount = new HerapinCapProductCountNotification(lineId, stationId, productCount, metric.Timestamp);
                    await PublishNotification(herapinCapProductCount, mediator);
                    break;
                case MessageType.EMessageType.HerapinCapMachineStatus:
                    var statusCode = (long)metric.Value;
                    var machineStatus = (EMachineStatus)statusCode;
                    var herapinCapMachineStatus = new HerapinCapMachineStatusChangedNotification(lineId, stationId, machineStatus, metric.Timestamp);
                    await PublishNotification(herapinCapMachineStatus, mediator);
                    break;
                case MessageType.EMessageType.CycleTime:
                    var cycleTime = (double)metric.Value;
                    var cycleTimeNotification = new CycleTimeNotification(lineId, stationId, cycleTime, metric.Timestamp);
                    await PublishNotification(cycleTimeNotification, mediator);
                    break;
                case MessageType.EMessageType.ExecutionTime:
                    var executionTime = (double)metric.Value;
                    var executionTimeNotification = new ExecutionTimeNotification(stationId, executionTime);
                    await PublishNotification(executionTimeNotification, mediator);
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

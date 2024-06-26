﻿using Microsoft.Identity.Client;
using WembleyScada.Domain.AggregateModels.LineAggregate;
using WembleyScada.Domain.AggregateModels.StationAggregate;

namespace WembleyScada.Api.Workers;
public class ScadaHost : BackgroundService
{
    #region Properties
    private readonly ManagedMqttClient _mqttClient;
    private readonly Buffer _buffer;
    private readonly ClientStorage _clientStorage;
    private readonly IHubContext<NotificationHub> _hubContext;
    #endregion

    #region Constructor & MQTT-related methods
    public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer, ClientStorage clientStorage ,IHubContext<NotificationHub> hubContext)
    {
        _mqttClient = mqttClient;
        _buffer = buffer;
        _clientStorage = clientStorage;
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

        //Mobile-AR
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+/+/+/+");
        await _mqttClient.Subscribe("Wembley/HerapinCap/IE-F2-HCA01/+/+/+/+/+/+/+");

        //Desktop
        await _mqttClient.Subscribe("WembleyMedical/+/+/Desktop/+");
        await _mqttClient.Subscribe("WembleyMedical/BTM/IE-F3-BLO06/Desktop/+");
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

        var lineId = topicSegments[1];
        var stationId = topicSegments[2];

        if (topic.Contains("Desktop"))
        {
            await SendMessageToDesktopClient(topic, stationId, lineId, metrics);
        }
        else
        {
            await SendMessageToMobileClient(topic, stationId, lineId, metrics);
        }
    }

    private async Task SendMessageToMobileClient(string topic, string stationId, string lineId, List<MetricMessage> metrics)
    {
        foreach (var metric in metrics)
        {
            var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
            await _buffer.Update(notification);
            string json = JsonConvert.SerializeObject(notification);

            var subscribedClients = await _clientStorage.GetSubcribedClientsByTopic(topic);

            if (subscribedClients.Count == 0) return;

            foreach (var subscribedClient in subscribedClients)
            {
                await _hubContext.Clients.Client(subscribedClient.ConnectionId).SendAsync("OnTagChanged", json);
            }

            //var publishTasks = subscribedClients.Select(client => _hubContext.Clients.Client(client.ConnectionId).SendAsync("OnTagChanged", notification)).ToArray();
            //await Task.WhenAll(publishTasks);
        }
    }

    private async Task SendMessageToDesktopClient(string topic, string stationId, string lineId, List<MetricMessage> metrics)
    {
        foreach (var metric in metrics)
        {
            var notification = new TagChangedNotification(stationId, lineId, metric.Name, metric.Value, metric.Timestamp);
            await _buffer.Update(notification);
            string json = JsonConvert.SerializeObject(notification);

            await _hubContext.Clients.All.SendAsync("TagChanged", json);

            var subscribedClients = await _clientStorage.GetSubcribedClientsByTopic(topic);

            if (subscribedClients.Count == 0) return;

            foreach (var subscribedClient in subscribedClients)
            {
                await _hubContext.Clients.Client(subscribedClient.ConnectionId).SendAsync("OnTagChanged", json);
            }
        }
    }
    #endregion
}

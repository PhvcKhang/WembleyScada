namespace WembleyScada.Infrastructure.Communication;

public class MqttMessage
{
    public string? Topic { get; set; }
    public string? Payload { get; set; }
    public MqttMessage(string topic, string payload)
    {
        Topic = topic;
        Payload = payload;
    }
}

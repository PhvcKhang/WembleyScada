namespace WembleyScada.Api.Application.Dtos
{
    public class MetricMessage
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public DateTime Timestamp { get; set; }

        public MetricMessage(string name, object value, DateTime timestamp)
        {
            Name = name;
            Value = value;
            Timestamp = timestamp;
        }
    }
}

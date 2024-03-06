namespace WembleyScada.Api.Application.Commands.DeviceReferences;

[DataContract]
public class UpdateMFCViewModel
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public double Value { get; set; }
    [DataMember]
    public double MinValue { get; set; }
    [DataMember]
    public double MaxValue { get; set; }

    public UpdateMFCViewModel(string name, double value, double minValue, double maxValue)
    {
        Name = name;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}

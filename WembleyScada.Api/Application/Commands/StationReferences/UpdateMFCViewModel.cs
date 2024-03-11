namespace WembleyScada.Api.Application.Commands.DeviceReferences;

[DataContract]
public class UpdateMFCViewModel
{
    [DataMember]
    public string MFCName { get; set; }
    [DataMember]
    public double Value { get; set; }
    [DataMember]
    public double MinValue { get; set; }
    [DataMember]
    public double MaxValue { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public UpdateMFCViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public UpdateMFCViewModel(string mFCname, double value, double minValue, double maxValue)
    {
        MFCName = mFCname;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}

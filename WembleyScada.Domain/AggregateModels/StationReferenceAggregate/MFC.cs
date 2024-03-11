
namespace WembleyScada.Domain.AggregateModels.StationReferenceAggregate;

public class MFC
{
    public string MFCId { get; private set; }
    public string MFCName { get; private set; }
    public double Value { get; private set; }
    public double MinValue { get; private set; }
    public double MaxValue { get; private set; }
    public string StationId { get; set; }
    public string ReferenceId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MFC() { }

    public MFC( string mFCName, double value, double minValue, double maxValue, string stationId, string referenceId)
    {
        MFCName = mFCName;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
        StationId = stationId;
        ReferenceId = referenceId;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public void UpdateMFCValues(MFC mFC)
    {
        Value = mFC.Value;
        MinValue = mFC.MinValue;
        MaxValue = mFC.MaxValue;
    }
}

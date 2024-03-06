namespace WembleyScada.Api.Application.Queries.StationReferences;

public class MFCViewModel
{
    public string MFCName { get; set; }
    public double Value { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public MFCViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public MFCViewModel(string mFCName, double value, double minValue, double maxValue)
    {
        MFCName = mFCName;
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}

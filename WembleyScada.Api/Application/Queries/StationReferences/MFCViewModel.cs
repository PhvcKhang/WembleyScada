namespace WembleyScada.Api.Application.Queries.StationReferences
{
    public class MFCViewModel
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public MFCViewModel(string name, double value, double minValue, double maxValue)
        {
            Name = name;
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}

namespace WembleyScada.Api.Application.Queries.References.Parameters;

public class ParameterViewModel
{
    public string ProductName { get; set; }
    public string ReferenceId { get; set; }
    public string ReferenceName { get; set; }
    public string LotCode { get; set; }
    public int LotSize { get; set; }
    public LineShortViewModel Line { get; set; }
    public List<StationInfoViewModel> Stations { get; set; }

    public ParameterViewModel(string productName, string referenceId, string referenceName, string lotCode, int lotSize, LineShortViewModel line, List<StationInfoViewModel> stations)
    {
        ProductName = productName;
        ReferenceId = referenceId;
        ReferenceName = referenceName;
        LotCode = lotCode;
        LotSize = lotSize;
        Line = line;
        Stations = stations;
    }
}

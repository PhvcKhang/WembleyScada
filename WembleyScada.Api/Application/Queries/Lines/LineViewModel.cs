
namespace WembleyScada.Api.Application.Queries.Lines;

public class LineViewModel
{
    public string LineId { get; set; }
    public string LineName { get; set; }
    public ELineType LineType { get;  set; }
    public List<StationViewModel> Stations { get;  set; }

    public LineViewModel(string lineId, string lineName, ELineType lineType, List<StationViewModel> stations)
    {
        LineId = lineId;
        LineName = lineName;
        LineType = lineType;
        Stations = stations;
    }
}

namespace WembleyScada.Api.Application.Queries.Stations;

public class StationViewModel
{
    public string StationId { get; set; }
    public string StationName { get; set; }
    public string LineId { get; set; }

    public StationViewModel(string stationId, string stationName, string lineId)
    {
        StationId = stationId;
        StationName = stationName;
        LineId = lineId;
    }
}

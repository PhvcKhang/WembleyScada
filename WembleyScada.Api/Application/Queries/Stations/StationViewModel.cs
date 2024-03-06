namespace WembleyScada.Api.Application.Queries.Stations;

public class StationViewModel
{
    public string StationId { get; private set; }
    public string StationName { get; private set; }
    public string LineId { get; private set; }

    public StationViewModel(string stationId, string stationName, string lineId)
    {
        StationId = stationId;
        StationName = stationName;
        LineId = lineId;
    }
}

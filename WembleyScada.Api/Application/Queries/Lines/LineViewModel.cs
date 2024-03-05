using WembleyScada.Domain.AggregateModels.LineAggregate;

namespace WembleyScada.Api.Application.Queries.Lines
{
    public class LineViewModel
    {
        public string LineId { get; private set; }
        public string LineName { get; private set; }
        public ELineType LineType { get; private set; }
        public List<StationViewModel> Stations { get; private set; }
        public LineViewModel(string lineId, string lineName, ELineType lineType, List<StationViewModel> stations)
        {
            LineId = lineId;
            LineName = lineName;
            LineType = lineType;
            Stations = stations;
        }
    }
}

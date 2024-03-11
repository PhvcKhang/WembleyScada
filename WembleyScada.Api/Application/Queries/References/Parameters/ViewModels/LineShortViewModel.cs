namespace WembleyScada.Api.Application.Queries.References.Parameters.ViewModels;

public class LineShortViewModel
{
    public string LineId { get; set; }
    public string LineName { get; set; }
    public ELineType LineType { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public LineShortViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public LineShortViewModel(string lineId, string lineName, ELineType lineType)
    {
        LineId = lineId;
        LineName = lineName;
        LineType = lineType;
    }
}

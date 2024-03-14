namespace WembleyScada.Api.Application.Queries.References;

public class ReferenceViewModel
{
    public string ReferenceId { get; set; }
    public string ReferenceName { get; set; }
    public string ProductName { get;  set; }
    public List<LineShortViewModel> UsableLines { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ReferenceViewModel() { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ReferenceViewModel(string referenceId, string referenceName, string productName, List<LineShortViewModel> usableLines)
    {
        ReferenceId = referenceId;
        ReferenceName = referenceName;
        ProductName = productName;
        UsableLines = usableLines;
    }
}

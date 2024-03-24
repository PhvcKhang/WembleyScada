namespace WembleyScada.Api.Application.Queries.StationReferences.Store;

public class StationReferenceStoreViewModel
{
    public string ReferenceId { get; set; }
    public string ReferenceName { get; set; }
    public string StationId { get; set; }
    public string StationName { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public StationReferenceStoreViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public StationReferenceStoreViewModel(string referenceId, string referenceName, string stationId, string stationName)
    {
        ReferenceId = referenceId;
        ReferenceName = referenceName;
        StationId = stationId;
        StationName = stationName;
    }
}

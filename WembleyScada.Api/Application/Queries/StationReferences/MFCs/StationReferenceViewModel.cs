namespace WembleyScada.Api.Application.Queries.StationReferences.MFCs;

public class StationReferenceViewModel
{
    public string StationId { get; set; }
    public List<MFCViewModel> MFCs { get; set; }

    public StationReferenceViewModel(string stationId, List<MFCViewModel> mFCs)
    {
        StationId = stationId;
        MFCs = mFCs;
    }
}

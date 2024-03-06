namespace WembleyScada.Api.Application.Commands.DeviceReferences;
public class UpdateStationReferenceCommand : IRequest<bool>
{
    public string ReferenceId { get; set; }
    public string StationId { get; set; }
    public List<UpdateMFCViewModel> MFCs { get; set; }

    public UpdateStationReferenceCommand(string referenceId, string stationId, List<UpdateMFCViewModel> mFCs)
    {
        ReferenceId = referenceId;
        StationId = stationId;
        MFCs = mFCs;
    }
}

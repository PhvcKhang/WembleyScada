namespace WembleyScada.Api.Application.Queries.References.Parameters.ViewModels;

public class StationInfoViewModel
{
    public string StationId { get; set; }
    public List<EmployeeWorkingViewModel> Employees { get; set; }
    public List<MFCViewModel> MFCs { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public StationInfoViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public StationInfoViewModel(string stationId, List<EmployeeWorkingViewModel> employees, List<MFCViewModel> mFCs)
    {
        StationId = stationId;
        Employees = employees;
        MFCs = mFCs;
    }
}

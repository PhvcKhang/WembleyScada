namespace WembleyScada.Api.Application.Queries.References.Parameters
{
    public class StationInfoViewModel
    {
        public string StationId { get; set; }
        public List<EmployeeWorkingViewModel> Persons { get; set; }
        public List<MFCViewModel> MFCs { get; set; }

        public StationInfoViewModel(string stationId, List<EmployeeWorkingViewModel> persons, List<MFCViewModel> mFCs)
        {
            StationId = stationId;
            Persons = persons;
            MFCs = mFCs;
        }
    }
}

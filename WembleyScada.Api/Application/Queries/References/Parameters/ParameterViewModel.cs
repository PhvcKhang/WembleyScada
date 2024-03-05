namespace WembleyScada.Api.Application.Queries.References.Parameters
{
    public class ParameterViewModel
    {
        public string ProductName { get; set; }
        public string ReferenceName { get; set; }
        public string LotCode { get; set; }
        public int LotSize { get; set; }
        public List<StationInfoViewModel> Stations { get; set; }

        public ParameterViewModel(string productName, string referenceName, string lotCode, int lotSize, List<StationInfoViewModel> stations)
        {
            ProductName = productName;
            ReferenceName = referenceName;
            LotCode = lotCode;
            LotSize = lotSize;
            Stations = stations;
        }
    }
}

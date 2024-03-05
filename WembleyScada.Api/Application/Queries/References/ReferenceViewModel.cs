namespace WembleyScada.Api.Application.Queries.References
{
    public class ReferenceViewModel
    {
        public string ReferenceId { get; set; }
        public string ReferenceName { get; set; }
        public string ProductName { get;  set; }

        public ReferenceViewModel(string referenceId, string referenceName, string productName)
        {
            ReferenceId = referenceId;
            ReferenceName = referenceName;
            ProductName = productName;
        }
    }
}

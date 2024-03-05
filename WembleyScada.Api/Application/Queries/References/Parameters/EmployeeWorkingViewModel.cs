namespace WembleyScada.Api.Application.Queries.References.Parameters
{
    public class EmployeeWorkingViewModel
    {
        public string EmployeId { get; set; }
        public string EmployeName { get; set; }

        public EmployeeWorkingViewModel(string employeId, string employeName)
        {
            EmployeId = employeId;
            EmployeName = employeName;
        }
    }
}

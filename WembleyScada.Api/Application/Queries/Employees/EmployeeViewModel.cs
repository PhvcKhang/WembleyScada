namespace WembleyScada.Api.Application.Queries.Employees;

public class EmployeeViewModel
{
    public string EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public List<WorkRecordViewModel> WorkRecords { get; set; }

    public EmployeeViewModel(string employeeId, string employeeName, List<WorkRecordViewModel> workRecords)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        WorkRecords = workRecords;
    }
}

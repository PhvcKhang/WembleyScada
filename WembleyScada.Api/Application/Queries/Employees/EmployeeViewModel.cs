namespace WembleyScada.Api.Application.Queries.Employees;

public class EmployeeViewModel
{
    public string EmployeeId { get; private set; }
    public string EmployeeName { get; private set; }
    public List<WorkRecordViewModel> WorkRecords { get; private set; }

    public EmployeeViewModel(string employeeId, string employeeName, List<WorkRecordViewModel> workRecords)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        WorkRecords = workRecords;
    }
}

namespace WembleyScada.Api.Application.Queries.References.Parameters.ViewModels;

public class EmployeeWorkingViewModel
{
    public string EmployeeId { get; set; }
    public string EmployeeName { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public EmployeeWorkingViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public EmployeeWorkingViewModel(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}

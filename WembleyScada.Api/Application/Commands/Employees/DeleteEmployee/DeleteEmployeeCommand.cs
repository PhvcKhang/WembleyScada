namespace WembleyScada.Api.Application.Commands.Employees.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<bool>
{
    public string EmployeeId { get; set; }

    public DeleteEmployeeCommand(string employeeId)
    {
        EmployeeId = employeeId;
    }
}

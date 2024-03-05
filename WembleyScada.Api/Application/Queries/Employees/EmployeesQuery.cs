namespace WembleyScada.Api.Application.Queries.Employees
{
    public class EmployeesQuery : IRequest<IEnumerable<EmployeeViewModel>>
    {
        public string? EmployeeId { get; }
    }
}

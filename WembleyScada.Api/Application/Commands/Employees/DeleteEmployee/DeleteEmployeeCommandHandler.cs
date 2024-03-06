
namespace WembleyScada.Api.Application.Commands.Employees.DeleteEmployee;

public class DeleteEmployeeCommandHandle : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandle(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeRepository.DeleteAsync(request.EmployeeId);
        return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

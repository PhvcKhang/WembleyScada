namespace WembleyScada.Api.Application.Commands.Employees.CreateEmployee;
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var Employee = new Employee(request.EmployeeId, request.EmployeeName);

            await _employeeRepository.CreateAsync(Employee);

            return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }


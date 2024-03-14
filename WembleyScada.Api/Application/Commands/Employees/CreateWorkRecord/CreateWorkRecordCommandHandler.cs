namespace WembleyScada.Api.Application.Commands.Employees.CreateEmployeeWorkRecord;
public class CreateWorkRecordCommandHandler : IRequestHandler<CreateWorkRecordCommand, bool>
{
    private IEmployeeRepository _employeeRepository;
    private IStationRepository _stationRepository;

    public CreateWorkRecordCommandHandler(IEmployeeRepository employeeRepository, IStationRepository stationRepository)
    {
        _employeeRepository = employeeRepository;
        _stationRepository = stationRepository;
    }

    public async Task<bool> Handle(CreateWorkRecordCommand request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetAsync(request.StationId) 
            ?? throw new ResourceNotFoundException(nameof(Station), request.StationId);

        foreach (var employeeId in request.EmployeeIds)
        {
            var employee = await _employeeRepository.GetAsync(employeeId) 
                ?? throw new ResourceNotFoundException(nameof(Employee), employeeId);

            employee.AddWorkRecord(station, EWorkStatus.Working, DateTime.UtcNow.AddHours(7));
        }

        return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


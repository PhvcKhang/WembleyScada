
namespace WembleyScada.Api.Application.Commands.Employees.UpdateWorkRecord;
public class UpdateWorkRecordCommandHandle : IRequestHandler<UpdateWorkRecordCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IStationRepository _stationRepository;

    public UpdateWorkRecordCommandHandle(IEmployeeRepository employeeRepository, IStationRepository stationRepository)
    {
        _employeeRepository = employeeRepository;
        _stationRepository = stationRepository;
    }

    public async Task<bool> Handle(UpdateWorkRecordCommand request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetAsync(request.StationId) 
            ?? throw new ResourceNotFoundException(nameof(Station), request.StationId);

        var workRecords = station.EmployeeWorkRecords
            .Where(x => x.WorkStatus == EWorkStatus.Working);

        var employeeIdsBeforeUpdated = workRecords
            .Select(x => x.EmployeeId)
            .ToList();

        foreach (var employeeId in employeeIdsBeforeUpdated)
        {
            var employee = await _employeeRepository.GetAsync(employeeId) 
                ?? throw new ResourceNotFoundException(nameof(Employee), employeeId);

            employee.DeleteWorkRecords();
        }

        var employeeIdsAfterUpdated = request.EmployeeIds;

        foreach (var employeeId in employeeIdsAfterUpdated)
        {
            var employee = await _employeeRepository.GetAsync(employeeId) 
                ?? throw new ResourceNotFoundException(nameof(Employee), employeeId);

            employee.AddWorkRecord(station, EWorkStatus.Working, DateTime.UtcNow.AddHours(7));
        }

        return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

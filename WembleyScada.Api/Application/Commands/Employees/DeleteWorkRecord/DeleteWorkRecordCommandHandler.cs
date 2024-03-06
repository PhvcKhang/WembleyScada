
namespace WembleyScada.Api.Application.Commands.Employees.DeleteWorkRecord
{
    public class DeleteWorkRecordCommandHandler : IRequestHandler<DeleteWorkRecordCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStationRepository _stationRepository;

        public DeleteWorkRecordCommandHandler(IEmployeeRepository employeeRepository, IStationRepository stationRepository)
        {
            _employeeRepository = employeeRepository;
            _stationRepository = stationRepository;
        }

        public async Task<bool> Handle(DeleteWorkRecordCommand request, CancellationToken cancellationToken)
        {
            var station = await _stationRepository.GetAsync(request.StationId) 
                ?? throw new ResourceNotFoundException(nameof(Station), request.StationId);

            foreach (var employeeId in request.EmployeeIds)
            {
                station.DeleteWorkRecords(employeeId);
            }

            return await _stationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}

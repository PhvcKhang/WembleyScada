
namespace WembleyScada.Api.Application.Commands.References;

public class UpdateParameterCommandHandler : IRequestHandler<UpdateParameterCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IStationRepository _stationRepository;

    public UpdateParameterCommandHandler(IReferenceRepository referenceRepository, IEmployeeRepository employeeRepository, IStationRepository stationRepository)
    {
        _referenceRepository = referenceRepository;
        _employeeRepository = employeeRepository;
        _stationRepository = stationRepository;
    }

    public async Task<bool> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.ReferenceName)
            ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.ReferenceName}' cannot be found.");
        
        reference.UpdateLotStatus(ELotStatus.Completed, DateTime.UtcNow.AddHours(7));

        var stations = new List<Station>();

        foreach (var line in reference.UsableLines)
        {
            var stationsOfLine = await _stationRepository.GetByLineTypeAsync(line.LineId);
            stations.AddRange(stationsOfLine);
        }

        foreach (var station in stations)
        {
            var workRecords = station.EmployeeWorkRecords
                .Where(x => x.WorkStatus == EWorkStatus.Working)
                .ToList();

            workRecords.ForEach(x => x.UpdateStatus(EWorkStatus.Completed, DateTime.UtcNow.AddHours(7)));
        }

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

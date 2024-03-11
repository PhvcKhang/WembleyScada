namespace WembleyScada.Api.Application.Commands.References.CreateLot;

public class CreateLotCommandHandler : IRequestHandler<CreateLotCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;
    private readonly IStationReferenceRepository _stationReferenceRepository;

    public CreateLotCommandHandler(IReferenceRepository referenceRepository, IStationReferenceRepository stationReferenceRepository)
    {
        _referenceRepository = referenceRepository;
        _stationReferenceRepository = stationReferenceRepository;
    }

    public async Task<bool> Handle(CreateLotCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.ReferenceName)
            ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.ReferenceName}' cannot be found.");

        var referencesOfLine= new List<Reference>();

        foreach (var line in reference.UsableLines)
        {
            var referenceItems = await _referenceRepository.GetByLineIdAsync(line.LineId);

            referencesOfLine.AddRange(referenceItems);
        }

        foreach (var referenceItem in referencesOfLine)
        {
            var workingLot = referenceItem.Lots.Find(x => x.LotStatus == ELotStatus.Working);

            if (workingLot is not null)
            {
                throw new Exception($"This Device is working with Lot {workingLot.LotId}, you cannot create additional Lots");
            }
        }

        reference.AddLot(request.LotCode, request.LotSize, ELotStatus.Working, DateTime.UtcNow.AddHours(7));

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

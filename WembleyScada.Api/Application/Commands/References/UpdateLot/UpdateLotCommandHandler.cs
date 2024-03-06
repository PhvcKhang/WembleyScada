
namespace WembleyScada.Api.Application.Commands.References;

public class UpdateLotCommandHandler : IRequestHandler<UpdateLotCommand, bool>
{
    private readonly IReferenceRepository _referenceRepository;

    public UpdateLotCommandHandler(IReferenceRepository referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }

    public async Task<bool> Handle(UpdateLotCommand request, CancellationToken cancellationToken)
    {
        var reference = await _referenceRepository.GetAsync(request.ReferenceName)
            ?? throw new ResourceNotFoundException($"The entity of type '{nameof(Reference)}' with Name '{request.ReferenceName}' cannot be found.");

        reference.UpdateLot(request.LotCode, request.LotSize);

        return await _referenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

namespace WembleyScada.Api.Application.Commands.StationReferences;

public class UpdateStationReferenceCommandHandler : IRequestHandler<UpdateStationReferenceCommand, bool>
{
    private readonly IStationReferenceRepository _stationReferenceRepository;
    private readonly IMapper _mapper;

    public UpdateStationReferenceCommandHandler(IStationReferenceRepository stationReferenceRepository, IMapper mapper)
    {
        _stationReferenceRepository = stationReferenceRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateStationReferenceCommand request, CancellationToken cancellationToken)
    {
        var stationReference = await _stationReferenceRepository.GetAsync(request.ReferenceId, request.StationId)
            ?? throw new ResourceNotFoundException($"The entity of type {nameof(StationReference)} with ReferenceId: {request.ReferenceId}, StationId: {request.StationId} can be found");

        var mFCs = _mapper.Map<List<MFC>>(request.MFCs);

        stationReference.UpdateMFCs(mFCs);

        return await _stationReferenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

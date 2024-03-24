namespace WembleyScada.Api.Application.Queries.StationReferences.Store;

public class StationReferenceStoresQueryHandler : IRequestHandler<StationReferenceStoresQuery, IEnumerable<StationReferenceStoreViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public StationReferenceStoresQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StationReferenceStoreViewModel>> Handle(StationReferenceStoresQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StationReferences
            .Include(x => x.Station)
            .Include(x => x.Reference)
            .AsNoTracking();

        var stationReferences = await queryable.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<StationReferenceStoreViewModel>>(stationReferences);

        return viewModels;
    }
}

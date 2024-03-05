
namespace WembleyScada.Api.Application.Queries.StationReferences
{
    public class StationReferencesQueryHandler : IRequestHandler<StationReferencesQuery, IEnumerable<StationReferenceViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StationReferencesQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StationReferenceViewModel>> Handle(StationReferencesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.StationReferences
                .Include(x => x.Station)
                .Include(x => x.Reference)
                .Include(x => x.MFCs)
                .AsNoTracking();

            if (request.ReferenceId is not null)
            {
                queryable = queryable.Where(x => x.ReferenceId == request.ReferenceId);
            }
            if (request.StationId is not null)
            {
                queryable = queryable.Where(x => x.StationId == request.StationId);
            }

            var deviceReferences = await queryable.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<StationReferenceViewModel>>(deviceReferences);
            
            return viewModels;
        }
    }
}

namespace WembleyScada.Api.Application.Queries.References;

public class ReferencesQueryHandler : IRequestHandler<ReferencesQuery, IEnumerable<ReferenceViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReferencesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReferenceViewModel>> Handle(ReferencesQuery request, CancellationToken cancellationToken)
    {
        var queryableReference = _context.References
            .Include(x => x.UsableLines)
            .Include(x => x.Product)
            .AsNoTracking();

        var references = new List<Reference>();

        var lines = await _context.Lines
            .AsNoTracking()
            .Where(x => x.LineType == request.LineType)
            .ToListAsync();

        if (request.LineType is not null && lines is not null)
        {
            foreach (var line in lines)
            {
                queryableReference = queryableReference
                    .Where(x => x.UsableLines.Contains(line));
            }
        }

        if (request.ProductId is not null)
        {
            references = await queryableReference
            .Where(x => x.ProductId == request.ProductId)
            .ToListAsync();
        }

        var viewModels = _mapper.Map<IEnumerable<ReferenceViewModel>>(references);
        return viewModels;
    }
}

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
        var queryable = _context.References
            .Include(x => x.UsableLines)
            .Include(x => x.Product)
            .AsNoTracking();

        var references = new List<Reference>();

        var line = await _context.Lines
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LineId == request.LineId);

        if (request.LineId is not null && line is not null)
        {
            queryable = queryable.Where(x => x.UsableLines.Contains(line));
        }

        if (request.ProductId is not null)
        {
            queryable = queryable.Where(x => x.ProductId == request.ProductId);
        }

        references = await queryable.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ReferenceViewModel>>(references);
        return viewModels;
    }
}

namespace WembleyScada.Api.Application.Queries.Lines;

public class LinesQueryHandler : IRequestHandler<LinesQuery, IEnumerable<LineViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LinesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LineViewModel>> Handle(LinesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Lines
            .Include(x => x.Stations)
            .AsNoTracking();

        if (request.LineId is not null)
        {
            queryable = queryable.Where(x => x.LineId == request.LineId);
        }

        var lines = await queryable.ToListAsync();
        var viewModels = _mapper.Map<IEnumerable<LineViewModel>>(lines);

        return viewModels;
    }
}

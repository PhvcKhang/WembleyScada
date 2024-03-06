
namespace WembleyScada.Api.Application.Queries.ShiftReports;

public class ShiftReportsQueryHandler: IRequestHandler<ShiftReportsQuery, IEnumerable<ShiftReportViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShiftReportsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShiftReportViewModel>> Handle(ShiftReportsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
            .Include(x => x.Shots)
            .Where(x => x.StationId == request.StationId
                     && x.Date >= request.StartTime
                     && x.Date <= request.EndTime)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.ShiftNumber)
            .AsNoTracking();

        queryable = queryable
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize);

        var shiftReports = await queryable.ToListAsync();
        var viewModels = _mapper.Map<IEnumerable<ShiftReportViewModel>>(shiftReports);

        return viewModels;
    }
}

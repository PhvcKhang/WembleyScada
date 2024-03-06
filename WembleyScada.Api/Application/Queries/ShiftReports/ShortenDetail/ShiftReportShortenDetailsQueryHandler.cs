
namespace WembleyScada.Api.Application.Queries.ShiftReports.ShortenDetail;

public class ShiftReportShortenDetailsQueryHandler : IRequestHandler<ShiftReportShortenDetailsQuery, IEnumerable<ShiftReportDetailViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShiftReportShortenDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShiftReportDetailViewModel>> Handle(ShiftReportShortenDetailsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
                .Include(x => x.Shots)
                .AsNoTracking();

        if (request.ShiftReportId is not null)
        {
            queryable = queryable.Where(x => x.ShiftReportId == request.ShiftReportId);
        }

        if (request.StationId is not null && request.Date is not null && request.ShiftNumber is not null)
        {
            queryable = queryable.Where(x => x.StationId == request.StationId
                                          && x.Date == request.Date
                                          && x.ShiftNumber == request.ShiftNumber);
        }

        var shiftReports = await queryable.ToListAsync();

        if (request.Interval != 1)
        {
            shiftReports.ForEach(x
                => x.Shots = x.Shots
                            .Where((x, index) => (index + 1) % request.Interval == 1)
                            .ToList());
        }

        return _mapper.Map<IEnumerable<ShiftReportDetailViewModel>>(shiftReports);
    }
}


namespace WembleyScada.Api.Application.Queries.ShiftReports.Lastest;

public class ShiftReportLatestDetailsQueryHandler : IRequestHandler<ShiftReportLatestDetailsQuery, IEnumerable<ShotOEEViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShiftReportLatestDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShotOEEViewModel>> Handle(ShiftReportLatestDetailsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
           .Include(x => x.Shots)
           .Where(x => x.StationId == request.StationId)
           .OrderByDescending(x => x.Date)
           .ThenByDescending(x => x.ShiftNumber)
           .AsNoTracking();

        var latestStatus = await _context.MachineStatus
           .Where(x => x.StationId == request.StationId)
           .OrderByDescending(x => x.Timestamp)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        var latestShiftReport = await queryable.FirstOrDefaultAsync();
        if(latestShiftReport is null) return new List<ShotOEEViewModel>();

        var shots = latestShiftReport.Shots;

        if (request.StationId is not null)
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Off)
            {
                shots.Clear();
            }
        }

        if (request.Interval != 1)
        {
            shots = shots.Where((x, index) => (index + 1) % request.Interval == 1).ToList();
        }

        var viewModels = _mapper.Map<IEnumerable<ShotOEEViewModel>>(shots);

        return viewModels;
    }
}

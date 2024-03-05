
namespace WembleyScada.Api.Application.Queries.ShiftReports.Details
{
    public class ShiftReportDetailsQueryHandler : IRequestHandler<ShiftReportDetailsQuery, IEnumerable<ShiftReportDetailViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShiftReportDetailsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftReportDetailViewModel>> Handle(ShiftReportDetailsQuery request, CancellationToken cancellationToken)
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

            foreach (var shiftReport in shiftReports)
            {
                shiftReport.Shots
                           .Skip((request.PageIndex - 1) * request.PageSize)
                           .Take(request.PageSize)
                           .ToList();
            }

            var viewModels = _mapper.Map<IEnumerable<ShiftReportDetailViewModel>>(shiftReports);
            
            return viewModels;
        }
    }
}

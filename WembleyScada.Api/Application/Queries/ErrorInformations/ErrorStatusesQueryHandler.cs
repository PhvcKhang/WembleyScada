
namespace WembleyScada.Api.Application.Queries.ErrorInformations;

public class ErrorStatusesQueryHandler: IRequestHandler<ErrorStatusesQuery, IEnumerable<ErrorStatusViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ErrorStatusesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ErrorStatusViewModel>> Handle(ErrorStatusesQuery request, CancellationToken cancellationToken)
    {
        var queryableErrors = _context.ErrorInformations
                .Include(x => x.ErrorStatuses)
                .AsNoTracking();


        var queryableErrorStatuses = queryableErrors.SelectMany(x =>
            x.ErrorStatuses.Where(x => x.Date >= request.StartTime
                                    && x.Date <= request.EndTime
                                    && x.Value == 1));

        var errorStatuses = await queryableErrorStatuses
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ErrorStatusViewModel>>(errorStatuses);

        return viewModels;
    }
}

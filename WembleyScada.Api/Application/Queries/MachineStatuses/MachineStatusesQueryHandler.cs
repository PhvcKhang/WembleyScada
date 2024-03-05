
namespace WembleyScada.Api.Application.Queries.MachineStatuses
{
    public class MachineStatusesQueryHandler: IRequestHandler<MachineStatusesQuery, IEnumerable<MachineStatusViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MachineStatusesQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MachineStatusViewModel>> Handle(MachineStatusesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.MachineStatus.AsNoTracking();

            if(request.StationId is not null)
            {
                queryable = queryable
                    .Where(x => x.StationId == request.StationId
                           && x.Date >= request.StartTime
                           && x.Date <= request.EndTime)
                    .OrderByDescending(x => x.Timestamp);
            }

            var machineStatuses = await queryable.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<MachineStatusViewModel>>(machineStatuses);

            return viewModels; 
        }
    }
}

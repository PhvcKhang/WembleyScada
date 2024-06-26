﻿
namespace WembleyScada.Api.Application.Queries.Stations;

public class StationsQueryHandler : IRequestHandler<StationsQuery, IEnumerable<StationViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public StationsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StationViewModel>> Handle(StationsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stations.AsNoTracking();

        if(request.LineId is not null)
        {
            queryable = queryable.Where(x => x.Line.LineId == request.LineId);
        }

        var stations = await queryable.ToListAsync();
        var viewModels = _mapper.Map<IEnumerable<StationViewModel>>(stations);

        return viewModels;
    }
}

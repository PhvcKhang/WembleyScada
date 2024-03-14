using WembleyScada.Api.Application.Queries.Lines;

namespace WembleyScada.Api.Application.Queries.References.Parameters;

public class ParametersQueryHandler : IRequestHandler<ParametersQuery, IEnumerable<ParameterViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ParametersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ParameterViewModel>> Handle(ParametersQuery request, CancellationToken cancellationToken)
    {
        var lines = await _context.Lines.ToListAsync();

        var queryable = _context.References
            .Include(x => x.UsableLines)
            .Include(x => x.Product)
            .Include(x => x.Lots)
            .AsNoTracking();

        if (request.LineId is not null)
        {
            lines = lines.Where(x => x.LineId == request.LineId).ToList();
        }

        if (request.ReferenceId is not null)
        {
            queryable = queryable.Where(x => x.ReferenceId == request.ReferenceId);
        }

        var viewModels = new List<ParameterViewModel>();

        foreach (var line in lines)
        {
            var references = await queryable
                .Where(x => x.UsableLines.Contains(line))
                .OrderByDescending(x => x.Lots.Any() ? x.Lots.Max(x => x.StartTime) : DateTime.MinValue)
                .ToListAsync();

            var lineViewModel = _mapper.Map<LineShortViewModel>(line);

            var tempViewModels = new List<ParameterViewModel>();

            foreach (var reference in references)
            {
                var lot = reference.Lots.Find(x => x.LotStatus == ELotStatus.Working);

                if (lot is null) continue;

                var viewModel = new ParameterViewModel(
                reference.Product.ProductName,
                reference.ReferenceId,
                reference.ReferenceName,
                lot.LotCode,
                lot.LotSize,
                lineViewModel,
                await MapToStationInfoViewModel(reference)
                );

                tempViewModels.Add(viewModel);
            }

            if (tempViewModels.Count == 0)
            {
                viewModels.Add(new ParameterViewModel(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                0,
                lineViewModel,
                new List<StationInfoViewModel>()
                ));
            }
            else
            {
                viewModels.AddRange(tempViewModels);    
            }
        }

        return viewModels;
    }

    private async Task<List<StationInfoViewModel>> MapToStationInfoViewModel(Reference reference)
    {
        var viewModels = new List<StationInfoViewModel>();

        var stationReferences = await _context.StationReferences
                .Include(x => x.Station)
                .ThenInclude(x => x.EmployeeWorkRecords)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Reference)
                .Include(x => x.MFCs)
                .Where(x => x.ReferenceId == reference.ReferenceId)
                .ToListAsync();

        foreach (var stationReference in stationReferences)
        {
            var employees = new List<Employee>();

            var workRecords = stationReference.Station
                .EmployeeWorkRecords
                .Where(x => x.WorkStatus == EWorkStatus.Working)
                .ToList();

            workRecords.ForEach(x => employees.Add(x.Employee));

            var viewModel = new StationInfoViewModel(
                stationReference.StationId,
                _mapper.Map<List<EmployeeWorkingViewModel>>(employees),
                _mapper.Map<List<MFCViewModel>>(stationReference.MFCs));

            viewModels.Add(viewModel);
        }

        return viewModels;
    }
}

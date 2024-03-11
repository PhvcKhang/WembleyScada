using WembleyScada.Api.Application.Queries.References.Parameters.ViewModels;

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
        var line = await _context.Lines
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LineId == request.LineId);

        var queryable = _context.References
            .Include(x => x.UsableLines)
            .Include(x => x.Product)
            .Include(x => x.Lots)
            .AsNoTracking();

        if (request.LineId is not null && line is not null)
        {
            queryable = queryable.Where(x => x.UsableLines.Contains(line));
        }

        if (request.ReferenceId is not null)
        {
            queryable = queryable.Where(x => x.ReferenceId == request.ReferenceId);
        }

        var references = await queryable.ToListAsync();

        references = references
            .GroupBy(x => x.UsableLines)
            .Select(group => group.OrderByDescending(x => x.Lots.Any() ? x.Lots.Max(x => x.StartTime) : DateTime.MinValue).First())
            .ToList();

        var viewModels = new List<ParameterViewModel>();

        foreach (var reference in references)
        {
            var lot = reference.Lots.Find(x => x.LotStatus == ELotStatus.Working);
            var lineViewModels = _mapper.Map<List<LineShortViewModel>>(reference.UsableLines);

            var viewModel = new ParameterViewModel(
                lot is null ? string.Empty : reference.Product.ProductName,
                reference.ReferenceId,
                lot is null ? string.Empty : reference.ReferenceName,
                lot is null ? string.Empty : lot.LotCode,
                lot is null ? 0 : lot.LotSize,
                lineViewModels,
                await MapToStationInfoViewModel(reference)
                );

            viewModels.Add(viewModel);
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

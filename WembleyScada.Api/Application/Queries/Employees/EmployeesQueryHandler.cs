namespace WembleyScada.Api.Application.Queries.Employees;

public class EmployeesQueryHandler : IRequestHandler<EmployeesQuery, IEnumerable<EmployeeViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmployeesQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeViewModel>> Handle(EmployeesQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Employees
            .Include(x => x.WorkRecords)
            .ThenInclude(x => x.Station)
            .AsNoTracking();

        if(request.EmployeeId is not null)
        {
            queryable = queryable.Where(x => x.EmployeeId == request.EmployeeId);
        }

        var employees = await queryable.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

        return viewModels;
    }
}

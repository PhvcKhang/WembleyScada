namespace WembleyScada.Infrastructure.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsExistedAsync(string employeeId)
    {
        return await _context.Employees
            .AnyAsync(x => x.EmployeeId == employeeId);
    }

    public async Task CreateAsync(Employee employee)
    {
        if(!await IsExistedAsync(employee.EmployeeId))
        {
            await _context.Employees.AddAsync(employee);
        }
    }
    public async Task<Employee?> GetAsync(string employeeId)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
    }

    public async Task DeleteAsync(string employeeId)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

        if(employee != null)
        {
            _context.Employees.Remove(employee);
        }
    }
}

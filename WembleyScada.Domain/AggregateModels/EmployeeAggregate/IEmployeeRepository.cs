using System;

namespace WembleyScada.Domain.AggregateModels.EmployeeAggregate;

public interface IEmployeeRepository : IRepository<Employee>
{
    public Task CreateAsync(Employee employee);
    public Task<Employee?> GetAsync(string employeeId);
    public Task DeleteAsync(string employeeId);
    public Task<bool> IsExistedAsync(string employeeId);
}

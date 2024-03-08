namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public interface IMachineStatusRepository : IRepository<MachineStatus>
{
    Task AddAsync(MachineStatus machineStatus);
    Task<MachineStatus?> GetLatestAsync(string stationId);
    Task<bool> IsExistedAsync(string stationId, DateTime timestamp);
}

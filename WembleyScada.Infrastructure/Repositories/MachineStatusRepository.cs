namespace WembleyScada.Infrastructure.Repositories;

public class MachineStatusRepository : BaseRepository, IMachineStatusRepository
{
    public MachineStatusRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task AddAsync(MachineStatus machineStatus)
    {
        if (!await IsExistedAsync(machineStatus.StationId, machineStatus.Timestamp))
            await _context.MachineStatus.AddAsync(machineStatus);
    }

    public async Task<MachineStatus?> GetLatestAsync(string stationId)
    {
        return await _context.MachineStatus
            .Where(x => x.StationId == stationId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistedAsync(string stationId, DateTime timestamp)
    {
        return await _context.MachineStatus
           .AnyAsync(x => x.StationId == stationId
                       && x.Timestamp == timestamp);
    }
}



namespace WembleyScada.Infrastructure.Repositories;

public class StationRepository : BaseRepository, IStationRepository
{
    public StationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Station?> GetAsync(string stationId)
    {
        return await _context.Stations
            .Include(x => x.EmployeeWorkRecords)
            .FirstOrDefaultAsync(x => x.StationId == stationId);
    }

    public async Task<IEnumerable<Station>> GetByLineTypeAsync(string lineId)
    {
        return await _context.Stations
            .Include(x => x.EmployeeWorkRecords)
            .Where(x => x.Line.LineId == lineId)
            .ToListAsync();
    }
}

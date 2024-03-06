

namespace WembleyScada.Infrastructure.Repositories;

public class StationRepository : BaseRepository, IStationRepository
{
    public StationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Station?> GetAsync(string stationId)
    {
        return await _context.Stations
            .FirstOrDefaultAsync(x => x.StationId == stationId);
    }

    public async Task<IEnumerable<Station>> GetByLineTypeAsync(ELineType lineType)
    {
        return await _context.Stations
            .Where(x => x.Line.LineType == lineType)
            .ToListAsync();
    }
}

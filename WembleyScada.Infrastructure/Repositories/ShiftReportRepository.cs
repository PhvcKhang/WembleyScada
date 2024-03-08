
namespace WembleyScada.Infrastructure.Repositories;
public class ShiftReportRepository : BaseRepository, IShiftReportRepository
{
    public ShiftReportRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ShiftReport?> GetLatestAsync(string stationId)
    {
        return await _context.ShiftReports
            .Include(x => x.Shots)
            .Include(x => x.Station)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.ShiftNumber)
            .FirstOrDefaultAsync(x => x.StationId == stationId);
    }

    public async Task AddAsync(ShiftReport shiftReport)
    {
        if(!await IsExistedAsync(shiftReport.StationId, shiftReport.ShiftNumber, shiftReport.Date))
            await _context.ShiftReports.AddAsync(shiftReport);
    }

    public async Task<bool> IsExistedAsync(string stationId, int shiftNumber, DateTime date)
    {
        return await _context.ShiftReports
            .AnyAsync(x => x.StationId == stationId
                        && x.ShiftNumber == shiftNumber
                        && x.Date == date);
    }

    public async Task<ShiftReport?> GetAsync(string stationId, int shiftNumber, DateTime date)
    {
        return await _context.ShiftReports
            .Include(x => x.Shots)
            .Include(x => x.Station)
            .FirstOrDefaultAsync(x => x.StationId == stationId 
                                   && x.ShiftNumber == shiftNumber 
                                   && x.Date == date);
    }

}

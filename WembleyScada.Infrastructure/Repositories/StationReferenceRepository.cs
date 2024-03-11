
namespace WembleyScada.Infrastructure.Repositories;

public class StationReferenceRepository : BaseRepository, IStationReferenceRepository
{
    public StationReferenceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<StationReference?> GetAsync(string referenceId, string stationId)
    {
        return await _context.StationReferences
            .Include(x => x.Station)
            .Include(x => x.Reference)
            .Include(x => x.MFCs)
            .FirstOrDefaultAsync(x => x.StationId == stationId && x.ReferenceId == referenceId);
    }
}

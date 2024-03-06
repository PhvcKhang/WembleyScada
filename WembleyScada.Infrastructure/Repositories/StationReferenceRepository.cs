
namespace WembleyScada.Infrastructure.Repositories;

public class StationReferenceRepository : BaseRepository, IStationReferenceRepository
{
    public StationReferenceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<StationReference?> GetAsync(string referenceId, string stationId)
    {
        return await _context.StationReferences
            .FirstOrDefaultAsync(x => x.StationId == referenceId && x.ReferenceId == referenceId);
    }
}

namespace WembleyScada.Infrastructure.Repositories;

public class ReferenceRepository : BaseRepository, IReferenceRepository
{
    public ReferenceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Reference?> GetAsync(string referenceName)
    {
        return await _context.References
            .Include(x => x.Lots)
            .Include(x => x.UsableLines)
            .FirstOrDefaultAsync(x => x.ReferenceName == referenceName);
    }

    public async Task<IEnumerable<Reference>> GetByLineIdAsync(string lineId)
    {
        var line = await _context.Lines
            .FirstOrDefaultAsync(x => x.LineId == lineId);

        if (line is null) return Enumerable.Empty<Reference>(); 

        return await _context.References
            .Include(x => x.Lots)
            .Where(x => x.UsableLines.Contains(line))
            .ToListAsync();
    }
}

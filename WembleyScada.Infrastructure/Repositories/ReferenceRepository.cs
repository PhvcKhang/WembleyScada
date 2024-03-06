


namespace WembleyScada.Infrastructure.Repositories;

public class ReferenceRepository : BaseRepository, IReferenceRepository
{
    public ReferenceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Reference?> GetAsync(string referenceName)
    {
        return await _context.References
            .FirstOrDefaultAsync(x => x.ReferenceName == referenceName);
    }

    public async Task<IEnumerable<Reference>> GetByLineTypeAsync(ELineType lineType)
    {
        var lines = _context.Lines
            .Where(x => x.LineType == lineType)
            .AsNoTracking();

        return await _context.References
            .Where(x => x.UsableLines == lines)
            .ToListAsync();
    }
}

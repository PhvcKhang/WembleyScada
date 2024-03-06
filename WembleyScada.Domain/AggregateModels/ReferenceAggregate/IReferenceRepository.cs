namespace WembleyScada.Domain.AggregateModels.ReferenceAggregate;

public interface IReferenceRepository : IRepository<Reference>
{
    Task<Reference?> GetAsync(string referenceName);
    Task<IEnumerable<Reference>> GetByLineTypeAsync (ELineType lineType);
}

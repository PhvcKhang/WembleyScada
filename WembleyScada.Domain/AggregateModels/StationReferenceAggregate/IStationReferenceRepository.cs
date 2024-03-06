
namespace WembleyScada.Domain.AggregateModels.StationReferenceAggregate;

public interface IStationReferenceRepository : IRepository<StationReference>
{
    Task<StationReference?> GetAsync(string referenceId, string stationId);
}

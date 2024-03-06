namespace WembleyScada.Domain.AggregateModels.StationAggregate;
    public interface IStationRepository : IRepository<Station>
    {
        Task<Station?> GetAsync(string stationId);
        Task<IEnumerable<Station>> GetByLineTypeAsync(ELineType lineType);
    }


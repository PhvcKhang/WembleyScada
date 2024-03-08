namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
public interface IShiftReportRepository : IRepository<ShiftReport>
{
    Task AddAsync (ShiftReport shiftReport);
    Task<ShiftReport?> GetLatestAsync(string stationId);
    Task<ShiftReport?> GetAsync(string stationId, int shiftNumber, DateTime date);
    Task<bool> IsExistedAsync(string stationId, int shiftNumber, DateTime date);
}

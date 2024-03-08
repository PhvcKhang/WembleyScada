
namespace WembleyScada.Host.Application.Commands.CycleTime;

public class CycleTimeNotificationHandler : INotificationHandler<CycleTimeNotification>
{
    private readonly MetricMessagePublisher _metricMessagePublisher;
    private readonly ExecutionTimeBuffers _executionTimeBuffers;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IStationRepository _stationRepository;

    public CycleTimeNotificationHandler(MetricMessagePublisher metricMessagePublisher,
                                        ExecutionTimeBuffers executionTimeBuffers,
                                        IShiftReportRepository shiftReportRepository,
                                        IStationRepository stationRepository)
    {
        _metricMessagePublisher = metricMessagePublisher;
        _executionTimeBuffers = executionTimeBuffers;
        _shiftReportRepository = shiftReportRepository;
        _stationRepository = stationRepository;
    }

    public async Task Handle(CycleTimeNotification notification, CancellationToken cancellationToken)
    {
        var station = await _stationRepository.GetAsync(notification.StationId);
        if (station is null) return;

        var shiftReport = new ShiftReport(station, notification.Timestamp);
        var existingShiftReport = await _shiftReportRepository.GetAsync(station.StationId, shiftReport.ShiftNumber, shiftReport.Date);

        if (existingShiftReport is not null)
        {
            shiftReport = existingShiftReport;
        }
        else
        {
            await _shiftReportRepository.AddAsync(shiftReport);
        }

        var executionTime = _executionTimeBuffers.GetStationLatestExecutionTime(notification.StationId);

        shiftReport.SetProductCount(shiftReport.Shots.Count);
        shiftReport.AddShot(executionTime, notification.CycleTime, notification.Timestamp, shiftReport.A, shiftReport.P, shiftReport.Q, shiftReport.OEE);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "products", shiftReport.ProductCount, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "counterShot", shiftReport.Shots.Count, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "operationTime", shiftReport.TotalCycleTime, notification.Timestamp);
    }
}

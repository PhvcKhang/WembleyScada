
namespace WembleyScada.Host.Application.Commands.CycleTime;

public class CycleTimeNotificationHandler : INotificationHandler<CycleTimeNotification>
{
    private readonly MetricMessagePublisher _metricMessagePublisher;
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IStationRepository _stationRepository;

    public CycleTimeNotificationHandler(MetricMessagePublisher metricMessagePublisher,
                                        IShiftReportRepository shiftReportRepository,
                                        StatusTimeBuffers statusTimeBuffers,
                                        IStationRepository stationRepository)
    {
        _metricMessagePublisher = metricMessagePublisher;
        _statusTimeBuffers = statusTimeBuffers;
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

        var startTime = _statusTimeBuffers.GetStartTime(notification.StationId);
        var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.StationId);
        var totalPreviousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.StationId);

        var runningTime = totalPreviousRunningTime + (notification.Timestamp - startRunningTime);
        var elapsedTime = notification.Timestamp - startTime;

        var cycleTime = notification.CycleTime;

        //When receive a "cycleTime" message, it means a shot have been created now, and productCount += 100
        var productCount = shiftReport.ProductCount + 100;
        shiftReport.SetProductCount(productCount);

        double A = runningTime / elapsedTime;
        double P = cycleTime * ((double)shiftReport.ProductCount / 100) / ((double)runningTime.TotalMilliseconds / 1000);

        shiftReport.SetA(A);
        shiftReport.SetP(P);
        shiftReport.SetElapsedTime(elapsedTime);

        //Logically, the following code line should have been placed before we set productCount, but we have to calculate the A, P first
        shiftReport.AddShot( cycleTime, notification.Timestamp, shiftReport.A, shiftReport.P, shiftReport.Q, shiftReport.OEE);

        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "products", shiftReport.ProductCount, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "ShotCounter", shiftReport.Shots.Count, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "operationTime", shiftReport.TotalCycleTime, notification.Timestamp);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

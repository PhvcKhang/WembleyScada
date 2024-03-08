namespace WembleyScada.Host.Application.Commands.HerapinCapsProductCount;

public class HerapinCapProductCountNotificationHandler : INotificationHandler<HerapinCapProductCountNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly IStationRepository _stationRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public HerapinCapProductCountNotificationHandler(StatusTimeBuffers statusTimeBuffers,
                                                     IMachineStatusRepository machineStatusRepository,
                                                     IShiftReportRepository shiftReportRepository,
                                                     IStationRepository stationRepository,
                                                     MetricMessagePublisher metricMessagePublisher)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _machineStatusRepository = machineStatusRepository;
        _shiftReportRepository = shiftReportRepository;
        _stationRepository = stationRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(HerapinCapProductCountNotification notification, CancellationToken cancellationToken)
    {
        var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.StationId);
        if (latestStatus is null || latestStatus.Status != EMachineStatus.Run) return;

        var station = await _stationRepository.GetAsync(notification.StationId);
        if (station is null) return;

        var shiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);
        if (shiftReport is null) return;

        var startTime = _statusTimeBuffers.GetStartTime(notification.StationId);
        var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.StationId);
        var totalPreviousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.StationId);

        var runningTime = totalPreviousRunningTime + (notification.Timestamp - startRunningTime);
        var elapsedTime = notification.Timestamp - startTime;

        double A = runningTime / elapsedTime;
        double P = 2.5 * (notification.ProductCount / 4) / (runningTime.TotalMilliseconds / 1000);

        shiftReport.SetA(A);
        shiftReport.SetP(P);
        shiftReport.SetElapsedTime(elapsedTime);
        shiftReport.SetProductCount(notification.ProductCount);
        shiftReport.AddHerapinCapShot(notification.Timestamp, shiftReport.A, shiftReport.P, shiftReport.Q, shiftReport.OEE);

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "A", shiftReport.A, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "P", shiftReport.P, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "Q", shiftReport.Q, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "OEE", shiftReport.OEE, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "operationTime", shiftReport.ElapsedTime, notification.Timestamp);
        await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "goodProduct", shiftReport.ProductCount - shiftReport.DefectCount, notification.Timestamp);
    }
}

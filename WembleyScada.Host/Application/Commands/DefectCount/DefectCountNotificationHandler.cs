
namespace WembleyScada.Host.Application.Commands.DefectCount;

public class DefectCountNotificationHandler : INotificationHandler<DefectCountNotification>
{
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public DefectCountNotificationHandler(IShiftReportRepository shiftReportRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _shiftReportRepository = shiftReportRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(DefectCountNotification notification, CancellationToken cancellationToken)
    {
        var shiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);
        if (shiftReport is null) return;

        shiftReport.SetDefectCount(notification.DefectCount);

        if (notification.DefectCount > 0)
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "Q", shiftReport.Q, notification.Timestamp);
            await _metricMessagePublisher.PublishMetricMessage(notification.LineId, notification.StationId, "OEE", shiftReport.OEE, notification.Timestamp);
        }

        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

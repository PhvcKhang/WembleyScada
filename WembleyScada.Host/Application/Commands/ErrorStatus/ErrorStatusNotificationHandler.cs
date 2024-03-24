namespace WembleyScada.Host.Application.Commands.ErrorStatus;

public class ErrorStatusNotificationHandler : INotificationHandler<ErrorStatusNotification>
{
    private readonly IErrorInformationRepository _errorInformationRepository;
    private readonly IShiftReportRepository _shiftReportRepository;
    private readonly MetricMessagePublisher _metricMessagePublisher;

    public ErrorStatusNotificationHandler(IErrorInformationRepository errorInformationRepository, IShiftReportRepository shiftReportRepository, MetricMessagePublisher metricMessagePublisher)
    {
        _errorInformationRepository = errorInformationRepository;
        _shiftReportRepository = shiftReportRepository;
        _metricMessagePublisher = metricMessagePublisher;
    }

    public async Task Handle(ErrorStatusNotification notification, CancellationToken cancellationToken)
    {
        var errorInformation = await _errorInformationRepository.GetAsync(notification.ErrorId);
        if (errorInformation is null) return;

        var shiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);
        if (shiftReport is null) return;

        errorInformation.AddErrorStatus(notification.Value, shiftReport.Date, shiftReport.ShiftNumber, notification.Timestamp);

        await _errorInformationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        if (notification.Value == 1)
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.LineId,
                                                   notification.StationId,
                                                   "errorStatus",
                                                   errorInformation.ErrorName,
                                                   notification.Timestamp);
        }
        else
        {
            await _metricMessagePublisher.PublishMetricMessage(notification.LineId,
                                                               notification.StationId,
                                                               "endErrorStatus",
                                                               errorInformation.ErrorName,
                                                               notification.Timestamp);
        }
    }
}

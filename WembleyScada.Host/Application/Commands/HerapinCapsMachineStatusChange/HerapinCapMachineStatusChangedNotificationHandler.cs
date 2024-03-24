namespace WembleyScada.Host.Application.Commands.HerapinCapsMachineStatusChange;

public class HerapinCapMachineStatusChangedNotificationHandler : INotificationHandler<HerapinCapMachineStatusChangedNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IStationRepository _stationRepository;
    private readonly IShiftReportRepository _shiftReportRepository;

    public HerapinCapMachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers,
                                                             IMachineStatusRepository machineStatusRepository,
                                                             IStationRepository stationRepository,
                                                             IShiftReportRepository shiftReportRepository)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _machineStatusRepository = machineStatusRepository;
        _stationRepository = stationRepository;
        _shiftReportRepository = shiftReportRepository;
    }

    public async Task Handle(HerapinCapMachineStatusChangedNotification notification, CancellationToken cancellationToken)
    {
        var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.StationId);

        var station = await _stationRepository.GetAsync(notification.StationId);

        if (station is null) return;

        if (notification.MachineStatus == EMachineStatus.On)
        {
            await HandleOnStatus(notification, station, cancellationToken);
        }
        else if (notification.MachineStatus == EMachineStatus.Run)
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run) return;
            _statusTimeBuffers.UpdateStartRunningTime(notification.StationId, notification.Timestamp);
        }
        else
        {
            HandleErrorStatus(notification, latestStatus);
        }

        await UpdateMachienStatus(notification, station, latestStatus, cancellationToken);
    }

    private async Task HandleOnStatus(HerapinCapMachineStatusChangedNotification notification, Station station, CancellationToken cancellationToken)
    {
        _statusTimeBuffers.UpdateStartTime(notification.StationId, notification.Timestamp);
        _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.StationId, TimeSpan.Zero);

        var latestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);

        var date = notification.Timestamp;
        int shiftNumber = latestShiftReport is null || date != latestShiftReport.Date ? 1 : latestShiftReport.ShiftNumber + 1;

        var shiftReport = new ShiftReport(shiftNumber, date, station);

        await _shiftReportRepository.AddAsync(shiftReport);
        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task UpdateMachienStatus(HerapinCapMachineStatusChangedNotification notification, Station station, MachineStatus? latestStatus, CancellationToken cancellationToken)
    {
        var latestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);

        if (latestShiftReport is null) return;

        var machineStatus = new MachineStatus(station,
                                              notification.MachineStatus,
                                              latestShiftReport.ShiftNumber,
                                              latestShiftReport.Date,
                                              notification.Timestamp);

        if (notification.MachineStatus == EMachineStatus.Off)
            machineStatus = new MachineStatus(station,
                                              notification.MachineStatus,
                                              latestShiftReport.ShiftNumber,
                                              latestShiftReport.Date,
                                              DateTime.UtcNow.AddHours(7));

        if (!await _machineStatusRepository.IsExistedAsync(notification.StationId, notification.Timestamp))
        {
            if (latestStatus is null || notification.MachineStatus != latestStatus.Status)
            {
                await _machineStatusRepository.AddAsync(machineStatus);
            }
        }

        await _machineStatusRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private void HandleErrorStatus(HerapinCapMachineStatusChangedNotification notification, MachineStatus? latestStatus)
    {
        if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run)
        {
            var previousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.StationId);

            var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.StationId);

            var runningTime = notification.Timestamp - startRunningTime;

            _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.StationId, previousRunningTime + runningTime);
        }
    }
}

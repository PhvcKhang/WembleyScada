namespace WembleyScada.Host.Application.Commands.MachineStatusChanged;

public class MachineStatusChangedNotificationHandler : INotificationHandler<MachineStatusChangedNotification>
{
    private readonly StatusTimeBuffers _statusTimeBuffers;
    private readonly IMachineStatusRepository _machineStatusRepository;
    private readonly IStationRepository _stationRepository;
    private readonly IShiftReportRepository _shiftReportRepository;

    public MachineStatusChangedNotificationHandler(StatusTimeBuffers statusTimeBuffers, IMachineStatusRepository machineStatusRepository, IStationRepository stationRepository, IShiftReportRepository shiftReportRepository)
    {
        _statusTimeBuffers = statusTimeBuffers;
        _machineStatusRepository = machineStatusRepository;
        _stationRepository = stationRepository;
        _shiftReportRepository = shiftReportRepository;
    }

    public async Task Handle(MachineStatusChangedNotification notification, CancellationToken cancellationToken)
    {
        var latestStatus = await _machineStatusRepository.GetLatestAsync(notification.StationId);

        var station = await _stationRepository.GetAsync(notification.StationId);

        if (station is null) return;

        if(notification.MachineStatus == EMachineStatus.On)
        {
            await HandleOnStatus(notification, station, cancellationToken);
        }
        else if(notification.MachineStatus == EMachineStatus.Run)
        {
            if (latestStatus is not null && latestStatus.Status == EMachineStatus.Run) return;
            _statusTimeBuffers.UpdateStartRunningTime(notification.StationId, notification.Timestamp);
        }
        else
        {
            await HandleOnErrorStatus(notification, latestStatus);
        }

        await UpdateMachineStatus(notification, station, latestStatus, cancellationToken);
    }

    private async Task UpdateMachineStatus(MachineStatusChangedNotification notification, Station station, MachineStatus? latestStatus, CancellationToken cancellationToken)
    {
        var latestShiftReport = await _shiftReportRepository.GetLatestAsync(notification.StationId);
        if(latestShiftReport is null) return;

        var timeStamp = notification.Timestamp;

        if (notification.MachineStatus == EMachineStatus.Off) timeStamp = DateTime.UtcNow.AddHours(7);

        var machineStatus = new MachineStatus(station, notification.MachineStatus, latestShiftReport.ShiftNumber, latestShiftReport.Date, timeStamp);
        
        if (!await _machineStatusRepository.IsExistedAsync(notification.StationId, notification.Timestamp))
        {
            if (latestStatus is null || notification.MachineStatus != latestStatus.Status)
            {
                await _machineStatusRepository.AddAsync(machineStatus);
            }
        }

        await _machineStatusRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task HandleOnStatus(MachineStatusChangedNotification notification, Station station, CancellationToken cancellationToken)
    {
        _statusTimeBuffers.UpdateStartTime(notification.StationId, notification.Timestamp);
        _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.StationId, TimeSpan.Zero);

        var shiftNumber = ShiftTimeHelper.GetShiftNumber(notification.Timestamp);
        var shiftDate = ShiftTimeHelper.GetShiftDate(notification.Timestamp);

        var shiftReport = new ShiftReport(shiftNumber, shiftDate, station);
        
        await _shiftReportRepository.AddAsync(shiftReport);
        await _shiftReportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
    private Task HandleOnErrorStatus(MachineStatusChangedNotification notification, MachineStatus? latestStatus)
    {
        if(latestStatus is not null && latestStatus.Status == EMachineStatus.Run)
        {
            var previousRunningTime = _statusTimeBuffers.GetTotalPreviousRunningTime(notification.StationId);

            var startRunningTime = _statusTimeBuffers.GetStartRunningTime(notification.StationId);

            var runningTime = notification.Timestamp - startRunningTime;

            _statusTimeBuffers.UpdateTotalPreviousRunningTime(notification.StationId, previousRunningTime + runningTime);
        }
        return Task.CompletedTask;
    }
}

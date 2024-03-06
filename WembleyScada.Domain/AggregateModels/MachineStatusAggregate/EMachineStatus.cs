namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public enum EMachineStatus
{
    On = 0,
    Run = 1,
    Idle = 2,
    Alarm = 3,
    Setup = 4,
    Off = 5,
    Ready = 6,
    WifiDisconnted = 7
}

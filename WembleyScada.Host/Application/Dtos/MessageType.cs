using WembleyScada.Domain.AggregateModels.LineAggregate;

namespace WembleyScada.Host.Application.Dtos;

public class MessageType
{
    public EMessageType Value { get; }
    public enum EMessageType
    {
        HerapinCapProductCount,
        HerapinCapMachineStatus,
        MachineStatus,
        CycleTime,
        ExecutionTime,
        DefectsCount,
        ErrorStatus,
        Unspecified
    }

    public MessageType(string lineId, MetricMessage message)
    {
        Value = (message.Name == "productCount" && lineId.StartsWith("HCM")) ? EMessageType.HerapinCapProductCount : EMessageType.Unspecified;

        Value = (message.Name == "machineStatus" && lineId.StartsWith("HCM")) ? EMessageType.HerapinCapMachineStatus : EMessageType.Unspecified;

        Value = (message.Name == "cycleTime") ? EMessageType.CycleTime : EMessageType.Unspecified;

        Value = (message.Name == "executionTime") ? EMessageType.ExecutionTime : EMessageType.Unspecified;

        Value = (message.Name == "errorProduct") ? EMessageType.DefectsCount : EMessageType.Unspecified;

        Value = (message.Name.StartsWith("M")) ? EMessageType.ErrorStatus : EMessageType.Unspecified;
    }
}

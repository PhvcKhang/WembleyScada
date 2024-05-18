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
        DefectsCount,
        ErrorStatus,
        Unspecified
    }

    public MessageType(string lineId, MetricMessage message)
    {
        if (message.Name == "productCount" && lineId.Contains("HCM")) Value = EMessageType.HerapinCapProductCount;
        else if (message.Name == "machineStatus")
        {
            if (lineId.Contains("HCM"))
            {
                Value = EMessageType.HerapinCapMachineStatus;
            }
            else
            {
                Value = EMessageType.MachineStatus;
            }
        }
        else if (message.Name.Contains("CYCLE_TIME")) Value = EMessageType.CycleTime;
        else if (message.Name == "errorProduct") Value = EMessageType.DefectsCount;
        else if (message.Name.StartsWith("M")) Value = EMessageType.ErrorStatus;
        else Value = EMessageType.Unspecified;
    }
}

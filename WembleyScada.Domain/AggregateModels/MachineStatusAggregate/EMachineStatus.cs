namespace WembleyScada.Domain.AggregateModels.MachineStatusAggregate;

public enum EMachineStatus
{
    On = 0, //Máy khởi động, có điện
    Run = 1, //Đang sản xuất
    Idle = 2, //Thời gian rảnh, có điện nhưng không chạy
    Alarm = 3, //Đang có vấn đề
    Setup = 4, //Đang cài đặt thông số
    Off = 5, //Đang tắt
    Ready = 6, //Không rõ
    WifiDisconnted = 7 //Ras Mất mạng
}

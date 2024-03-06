namespace WembleyScada.Api.Application.Commands.References.CreateLot;

[DataContract]
public class CreateLotViewModel
{
    [DataMember]
    public string LotCode { get; set; }
    [DataMember]
    public int LotSize { get; set; }

    public CreateLotViewModel(string lotCode, int lotSize)
    {
        LotCode = lotCode;
        LotSize = lotSize;
    }
}

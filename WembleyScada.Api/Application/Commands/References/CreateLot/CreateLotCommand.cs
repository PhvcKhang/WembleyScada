namespace WembleyScada.Api.Application.Commands.References.CreateLot;

public class CreateLotCommand : IRequest<bool>
{
    public string ReferenceName { get; set; }
    public string LotCode { get; set; }
    public int LotSize { get; set; }

    public CreateLotCommand(string referenceName, string lotCode, int lotSize)
    {
        ReferenceName = referenceName;
        LotCode = lotCode;
        LotSize = lotSize;
    }
}

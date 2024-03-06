namespace WembleyScada.Api.Application.Commands.References;

public class UpdateLotCommand : IRequest<bool>
{
    public string ReferenceName { get; set; }
    public string LotCode { get; set; }
    public int LotSize { get; set; }
    public UpdateLotCommand(string referenceName, string lotCode, int lotSize)
    {
        ReferenceName = referenceName;
        LotCode = lotCode;
        LotSize = lotSize;
    }
}

using System.Runtime.Serialization;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.References;

[DataContract]
public class UpdateLotViewModel
{
    [DataMember]
    public string LotCode { get; set; }
    [DataMember]
    public int LotSize { get; set; }

    public UpdateLotViewModel(string lotCode, int lotSize)
    {
        LotCode = lotCode;
        LotSize = lotSize;
    }
}

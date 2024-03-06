namespace WembleyScada.Api.Application.Commands.References;

public class UpdateParameterCommand : IRequest<bool>
{
    public string ReferenceName { get; set; }

    public UpdateParameterCommand(string referenceName)
    {
        ReferenceName = referenceName;
    }
}

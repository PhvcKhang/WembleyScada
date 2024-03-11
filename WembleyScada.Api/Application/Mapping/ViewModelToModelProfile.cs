namespace WembleyScada.Api.Application.Mapping;
public class ViewModelToModelProfile : Profile
{
    public ViewModelToModelProfile()
    {
        CreateMap<UpdateMFCViewModel, MFC>();
    }
}

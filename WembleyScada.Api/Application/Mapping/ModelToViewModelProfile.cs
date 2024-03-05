using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Api.Application.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<Station, StationViewModel>();
            CreateMap<Line, LineViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<StationReference, StationReferenceViewModel>();
            CreateMap<MFC, MFCViewModel>();
            CreateMap<MachineStatus, MachineStatusViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Employee, EmployeeWorkingViewModel>();
            CreateMap<WorkRecord, WorkRecordViewModel>();
            CreateMap<ShiftReport, ShiftReportViewModel>();
            CreateMap<ShiftReport, ShiftReportDetailViewModel>();
            CreateMap<Shot, ShotViewModel>();
            CreateMap<Shot, ShotOEEViewModel>();

            CreateMap<Reference, ReferenceViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<ErrorStatus, ErrorStatusViewModel>()
                .ForMember(dest => dest.ErrorName, opt => opt.MapFrom(src => src.ErrorInformation.ErrorName));
        }
    }
}

using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BillingCustomerViewModel, BillingCustomerEntity>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyEntity>().ReverseMap();
            CreateMap<SubsidiaryViewModel, SubsidiaryEntity>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeEntity>().ReverseMap();
            CreateMap<UserEntity, UserEntity>().ReverseMap();
            CreateMap<PointTimeViewModel, PointTimeEntity>().ReverseMap();

        }
    }
}

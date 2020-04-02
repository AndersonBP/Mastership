using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Entities;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BillingCustomerViewModel, BillingCustomerDTO>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyDTO>().ReverseMap();
            CreateMap<SubsidiaryViewModel, SubsidiaryDTO>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeDTO>().ReverseMap();
            CreateMap<UserViewModel, UserDTO>().ReverseMap();
            CreateMap<PointTimeViewModel, PointTimeDTO>().ReverseMap();

            
            CreateMap<BillingCustomerViewModel, BillingCustomerEntity>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyEntity>().ReverseMap();
            CreateMap<SubsidiaryViewModel, SubsidiaryEntity>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeEntity>().ReverseMap();
            CreateMap<UserViewModel, UserEntity>().ReverseMap();
            CreateMap<PointTimeViewModel, PointTimeEntity>().ReverseMap();

        }
    }
}

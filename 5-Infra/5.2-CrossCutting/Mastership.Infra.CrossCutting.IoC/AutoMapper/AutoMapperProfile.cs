using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Infra.Data.Entities;
using Mastership.Domain.ViewModels;

namespace Mastership.Infra.CrossCutting.IoC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeViewModel, CheckRegistrationViewModel>().ForMember(x => x.QuestionType, opt => opt.Ignore()).ReverseMap();
            

            CreateMap<BillingCustomerViewModel, BillingCustomerDTO>().ReverseMap();
            CreateMap<CompanyViewModel, CompanyDTO>().ReverseMap();
            CreateMap<SubsidiaryViewModel, SubsidiaryDTO>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeDTO>().ReverseMap();
            CreateMap<UserViewModel, UserDTO>().ReverseMap();
            CreateMap<PointTimeViewModel, PointTimeDTO>().ForMember(x=>x.Employee, opt=> opt.Ignore()).ReverseMap();

            
            CreateMap<BillingCustomerDTO, BillingCustomerEntity>().ReverseMap();
            CreateMap<CompanyDTO, CompanyEntity>().ReverseMap();
            CreateMap<SubsidiaryDTO, SubsidiaryEntity>().ReverseMap();
            CreateMap<EmployeeDTO, EmployeeEntity>().ReverseMap();
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<PointTimeDTO, PointTimeEntity>().ReverseMap();

        }
    }
}
